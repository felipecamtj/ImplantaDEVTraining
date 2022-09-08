Profissionais_IndexViewModel = function () {
    var self = this;

    self.Filtro = ko.observable();
    self.Registros = ko.observableArray();
    self.Pesquisando = ko.observable(false);

    self.BuscarFiltro = function () {
        const url = '/Profissionais/BuscarFiltro';
        const ajaxParams = {};
        const ajaxCallbackFunction = (response) => {
            if (!response) {
                alert('Erro ao buscar filtro!');
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

        const url = '/Profissionais/BuscarListagem';
        const ajaxParams = ko.toJS(self.Filtro);
        const ajaxCallbackFunction = (response) => {

            self.Pesquisando(false);

            if (!response) {                
                alert('Erro ao buscar registros!');
                return;
            }

            const registros = response.data;
            ko.utils.arrayForEach(registros, (registro) => self.Registros.push(ko.viewmodel.fromModel(registro)));

            if (registros.length === 0) {
                alert('Nenhum dado encontrado.');
            }
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
        window.location.href = `/Profissionais/Editar/${registro.Id()}`;
    };

    self.btnNovoClick = function () {
        window.location.href = '/Profissionais/Editar';
    };

    self.TextoBtnPesquisar = ko.pureComputed(function () {
        return self.Pesquisando()
            ? 'Aguarde...'
            : 'Pesquisar';
    }, self);

    self.BuscarFiltro();
}