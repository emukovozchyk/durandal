define(['plugins/router', 'knockout'], function (router, ko) {

    function ViewModel() {
        var self = this;
            
        self.editName = ko.observable('');
        self.editAge = ko.observable('');
        self.bufferedItem = ko.observable('');

        self.activate = function (passedId) {
            return $.getJSON('/api/testapi/GetPerson/' + passedId).done(function (data) {
                self.bufferedItem = data;
                self.editName(self.bufferedItem.Name);
                self.editAge(self.bufferedItem.Age);
            });
        };
        
        self.editPerson = function () {
            self.bufferedItem.Name = self.editName();
            self.bufferedItem.Age = self.editAge();
            $.ajax("/api/testapi/PutPerson?id=" + self.bufferedItem.Id, {
                data: ko.toJSON(self.bufferedItem),
                type: "put",
                contentType: "application/json"
            }).then(function() {
                router.navigate('');
            });
        };
    }
    return ViewModel;
});