define(['plugins/router'], function (router) {
    return {
        router: router,
        activate: function () {
            router.map([
                { route: '', title: 'People', moduleId: 'demo1/viewmodels/people', nav: true },
                { route: 'add', moduleId: 'demo1/viewmodels/add' },
                { route: 'edit/:id', moduleId: 'demo1/viewmodels/edit' },
                { route: 'about', moduleId: 'demo1/viewmodels/about', nav: true }
            ]).buildNavigationModel();

            return router.activate();
        }
    };
});