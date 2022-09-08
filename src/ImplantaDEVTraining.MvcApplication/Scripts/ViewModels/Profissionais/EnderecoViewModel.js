EnderecoViewModel = function (fnGetEnderecosArray) {

    var self = this;

    self.Endereco = ko.observable();
    self.fnEnderecosArray = fnGetEnderecosArray;

    self.AbrirModal = function () {
        $('#modalEndereco').modal();
    };

    self.FecharModal = function () {
        $('#modalEndereco').modal('hide');
    };

    self.BuscarNovoEnderecoObj = function (idProfissional) {
        return new Promise((resolve, reject) => {
            const url = '/Enderecos/BuscarNovoEndereco';
            const ajaxParams = { idProfissional };
            const ajaxFunctionCallback = function (response) {

                if (!response) {
                    alert('Erro ao buscar novo endereço!');
                    reject();
                    return;
                }

                self.Endereco(ko.viewmodel.fromModel(response.data));
                resolve();
            };

            $.getJSON(url, ajaxParams, ajaxFunctionCallback);
        });
    };

    self.AdicionarEndereco = function (idProfissional) {
        this.BuscarNovoEnderecoObj(idProfissional)
            .then(() => self.AbrirModal());
    };

    self.AlterarEndereco = function (endereco) {
        const copia = ko.viewmodel.fromModel(ko.toJS(endereco));
        self.Endereco(copia);

        if (self.Endereco().Acao() !== $.EntityAction.New) {
            self.Endereco().Acao($.EntityAction.Update);
        }

        self.AbrirModal();
    };

    self.ConfirmarExclusao = function () {
        return new Promise((resolve, reject) => {
            const response = confirm('Deseja realmente excluir o endereço da lista?');

            if (response) {
                resolve();
            } else {
                reject();
            }
        });
    };

    self.ExcluirEndereco = function (endereco) {
        const fnExcluirEnderecoCallback = function () {
            const enderecosArray = self.fnEnderecosArray();

            if (endereco.Acao() === $.EntityAction.New) {
                enderecosArray.remove(endereco);
            } else {
                endereco.Acao($.EntityAction.Delete);
            }
        };

        self.ConfirmarExclusao()
            .then(() => fnExcluirEnderecoCallback());
    };

    self.Validar = function () {
        let result = $("#formEndereco").valid();
        return result;
    };

    self.BtnSalvarEnderecoClick = function () {
        if (!self.Validar()) {
            return;
        }

        const enderecosArray = self.fnEnderecosArray();
        const itemOriginal = ko.utils.arrayFirst(enderecosArray(), (e) => e.Id() === self.Endereco().Id());

        if (itemOriginal) {
            ko.viewmodel.updateFromModel(itemOriginal, ko.toJS(self.Endereco()));
        } else {
            enderecosArray.push(self.Endereco());
        }

        self.FecharModal();
    };
};