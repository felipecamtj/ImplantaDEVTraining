Profissionais_EditarViewModel = function (id) {
    var self = this;

    self.Id = ko.observable(id);
    self.Model = ko.observable();

    self.GetEnderecosArray = function () {
        return self.Model().Enderecos;
    };

    self.EnderecoViewModel = new EnderecoViewModel(self.GetEnderecosArray);

    self.BuscarRegistro = function () {
        const url = '/Profissionais/BuscarRegistro';
        const ajaxParams = { id: self.Id() };
        const ajaxFunctionCallback = function (response) {

            if (!response) {
                alert('Erro ao buscar dados!');
                return;
            }

            if (!response.data.Enderecos) {
                response.data.Enderecos = [];
            }

            self.Model(ko.viewmodel.fromModel(response.data));
        };

        $.getJSON(url, ajaxParams, ajaxFunctionCallback);
    };

    self.Salvar = function () {
        const url = '/Profissionais/Salvar';
        const ajaxParams = { entity: ko.toJS(self.Model) };
        const ajaxFunctionCallback = function (response) {

            if (!response) {
                alert('Erro ao salvar!');
                return;
            }

            const operacao = response.data;

            if (operacao.Erro) {
                const msg = operacao.Mensagens.join(', ');
                alert(msg);
                return;
            }

            ko.viewmodel.updateFromModel(self.Model, operacao.Entidade);
            alert('Salvo com sucesso!');
        };

        $.post(url, ajaxParams, ajaxFunctionCallback, 'json');
    };

    self.BtnCancelarClick = function () {
        window.location.href = '/Profissionais';
    };

    self.BtnSalvarClick = function () {
        if (self.Validar()) {
            self.Salvar();
        }
    };

    self.Validar = function () {
        isValid = $("#formProfissionais").valid();
        return isValid;
    };

    self.BtnExcluirVisible = ko.pureComputed(function () {
        const actionDataNew = 1;
        return self.Model() && self.Model().Acao() !== actionDataNew;
    }, self);

    self.ConfirmarExclusao = function () {
        return new Promise((resolve, reject) => {
            const confirmacao = confirm('Deseja realmente excluir este registro?');

            if (confirmacao) {
                resolve();
            } else {
                reject();
            }
        });
    };

    self.Excluir = function () {

        const actionDataExcluir = 3;
        const model = ko.toJS(self.Model);
        model.Acao = actionDataExcluir;

        const url = '/Profissionais/Salvar';
        const ajaxParams = { entity: model };
        const ajaxFunctionCallback = function (response) {

            if (!response) {
                alert('Erro ao excluir!');
                return;
            }

            const operacao = response.data;

            if (operacao.Erro) {
                const msg = operacao.Mensagens.join(', ');
                alert(msg);
                return;
            }

            self.BtnCancelarClick();
        };

        $.post(url, ajaxParams, ajaxFunctionCallback, 'json');
    };

    self.BtnExcluirClick = function () {
        self.ConfirmarExclusao()
            .then(() => self.Excluir());
    };

    self.BtnAdicionarEnderecoClick = function () {
        const idProfissional = self.Model().Id();
        self.EnderecoViewModel.AdicionarEndereco(idProfissional);
    };

    self.BtnEditarEnderecoClick = function (endereco) {
        self.EnderecoViewModel.AlterarEndereco(endereco);
    };

    self.BtnExcluirEnderecoClick = function (endereco) {
        self.EnderecoViewModel.ExcluirEndereco(endereco);
    };

    self.DetalharEndereco = function (endereco) {
        return `${endereco.Logradouro()}, núm. ${endereco.Numero()} - ${endereco.Bairro()} - ${endereco.Cidade()}`;
    };

    $.BuscarCategorias()
        .then(() => self.BuscarRegistro());
};