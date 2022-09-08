Categorias_EditarViewModel = function (id) {

    debugger;

    var self = this;

    self.Id = ko.observable(id);
    self.Model = ko.observable();

    self.BuscarRegistro = function () {
        const url = '/Categorias/BuscarRegistro';
        const ajaxParams = { id: self.Id() };

        const ajaxCallbackFunction = (response) => {

            if (!response) {
                alert('Erro ao buscar dados!');
                return;
            }

            self.Model(ko.viewmodel.fromModel(response.data));
        };

        $.getJSON(url, ajaxParams, ajaxCallbackFunction);
    };

    self.Salvar = function () {
        const url = '/Categorias/Salvar';
        const ajaxParams = { entity: ko.toJS(self.Model) };
        const ajaxCallbackFunction = (response) => {

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

        $.post(url, ajaxParams, ajaxCallbackFunction);
    };

    self.Validar = function () {
        return $('#formCategorias').valid();        
    };

    self.BtnSalvarClick = function () {
        if (self.Validar()) {
            self.Salvar();
        }
    };

    self.RedirecionarParaPaginaDeBusca = function () {
        window.location.href = '/Categorias';
    };

    self.BtnCancelarClick = function () {
        self.RedirecionarParaPaginaDeBusca();
    };

    self.BtnExcluirVisible = ko.pureComputed(function () {
        return self.Model() && self.Model().Acao() !== $.EntityAction.New;
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

        const model = ko.toJS(self.Model);

        model.Acao = $.EntityAction.Delete;

        const url = '/Categorias/Salvar';
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

            self.RedirecionarParaPaginaDeBusca();
        };

        $.post(url, ajaxParams, ajaxFunctionCallback, 'json');
    };

    self.BtnExcluirClick = function () {
        self.ConfirmarExclusao()
            .then(() => self.Excluir());
    };

    self.BuscarRegistro();
}