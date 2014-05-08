define(['plugins/router', 'knockout', 'knockout.validation', 'toastr'], function (router, ko, validation, toastr) {
    
    ko.validation.init({
        insertMessages: false
    });

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "positionClass": "toast-bottom-right",
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    var ViewModel = {        
        editName: ko.observable('').extend({ required: true, minLength: 5, maxLength: 20 }),
        editAge: ko.observable('').extend({ required: true, max: 100, min: 18 }),

        bufferedItem: ko.observable(''),

        activate: function(passedId) {
            var self = this;
            return $.getJSON('/api/testapi/GetPerson/' + passedId).done(function(data) {
                self.bufferedItem = data;
                self.editName(self.bufferedItem.Name);
                self.editAge(self.bufferedItem.Age);
            });
        },

        editPerson: function () {
            if (ViewModel.errors().length == 0) {
                var self = this;
                self.bufferedItem.Name = self.editName();
                self.bufferedItem.Age = self.editAge();
                $.ajax("/api/testapi/PutPerson?id=" + self.bufferedItem.Id, {
                    data: ko.toJSON(self.bufferedItem),
                    type: "put",
                    contentType: "application/json",
                    success: function() {
                        toastr.success('Saved!', 'Edit person');
                    },
                    error: function() {
                        toastr.error('Something go wrong!', 'Edit person');
                    }
                }).then(function() {
                    router.navigate('');
                });
            } else {
                toastr.warning('Validation errors!', 'Add new person');
                ViewModel.errors.showAllMessages();
            }
        }
    };
    
    ViewModel.errors = ko.validation.group(ViewModel);

    return ViewModel;
});