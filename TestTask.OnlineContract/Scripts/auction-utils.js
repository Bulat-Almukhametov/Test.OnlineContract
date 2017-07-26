$(function () {
    function showMessage(text) {
        $(".modal-content").html(text);

        $(".auctionModalBox").modal("show");
    }

    var chat = $.connection.auctionHub;

    var viewModel = new (function () {
        this.thisUser = ko.observable(""); 
        this.users = ko.observableArray();


        this.price = ko.observable(100);
        this.offerPrice = ko.observable(105);

        this.fullExpression = ko.computed(function () {
            return "";
        }, this);
    })();

    ko.applyBindings(viewModel);

    viewModel.price.subscribe(function () {
        viewModel.offerPrice._latestValue = viewModel.price() + 5;
        viewModel.offerPrice.valueHasMutated();
    });

    chat.client.onConnected = function (me, allUsers) {
        viewModel.users._latestValue = allUsers;
        viewModel.users.valueHasMutated();

        viewModel.thisUser._latestValue = me;
        viewModel.thisUser.valueHasMutated(); 
    };

    chat.client.changeUserList = function (allUsers) {

        debugger;
    };

    chat.client.startAuction = function (price) {
        viewModel.price._latestValue = price;
        viewModel.price.valueHasMutated(); 
    }

    chat.client.changePrice = function (price) {
        viewModel.price._latestValue = price;
        viewModel.price.valueHasMutated();
    }

    chat.client.announceTheWinner = function (price, userName) {
        showMessage("Продано! Пользователю: " + userName +". Цена лота - " + price + " p.");
        
    }

    chat.client.cangratulate = function (price) {
        showMessage("Поздравляем с покупкой! Цена - " + price + " p.");

    }

    chat.client.stopAuction = function (state) {
        showMessage("Аукцион завершился!");

    }





    $.connection.hub.start().done(function () {
        chat.server.connect();
    });

    $(".tab-content .btn").click(function () {
        chat.server.makeOffer(viewModel.offerPrice());
    });
});