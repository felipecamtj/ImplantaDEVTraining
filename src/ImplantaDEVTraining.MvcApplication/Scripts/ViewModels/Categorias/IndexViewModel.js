Categorias_IndexViewModel = function () {
    var self = this;

    self.Filtro = ko.observable();
    self.Registros = ko.observableArray();
    self.Pesquisando = ko.observable(false);

    self.BuscarFiltro = function () {
        const url = '/Categorias/BuscarFiltro';
        const ajaxParams = {};
        const ajaxCallbackFunction = (response) => {
            if (!response) {
                alert('Erro ao buscar filtro de categorias!');
                return;
            }

            const filtro = response.data;
            self.Filtro(ko.viewmodel.fromModel(filtro));
        };

        $.getJSON(url, ajaxParams, ajaxCallbackFunction);
    };

    self.BuscarRegistros = function () {

        self.Pesquisando(true);
        self.Registros.removeAll();

        const url = '/Categorias/BuscarRegistros';
        const ajaxParams = ko.toJS(self.Filtro);
        const ajaxCallbackFunction = (response) => {

            self.Pesquisando(false);

            if (!response) {
                alert('Erro ao buscar registros!');
                return;
            }

            const registros = response.data;
            ko.utils.arrayForEach(registros, (registro) => self.Registros.push(ko.viewmodel.fromModel(registro)));
        };

        $.getJSON(url, ajaxParams, ajaxCallbackFunction);
    };

    self.ExistemDados = ko.pureComputed(function () {
        return self.Registros().length > 0;
    }, self);

    self.btnPesquisarClick = function () {
        self.BuscarRegistros();
    };

    self.btnEditarClick = function (registro) {
        window.location.href = `/Categorias/Editar/${registro.Id()}`;
    };

    self.btnNovoClick = function () {
        window.location.href = '/Categorias/Editar';
    };

    self.TextoBtnPesquisar = ko.pureComputed(function () {
        return self.Pesquisando()
            ? 'Aguarde ...'
            : 'Pesquisar';
    }, self);

    self.BuscarFiltro();
};