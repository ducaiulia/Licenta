var SocialSpace = {
    HideModal: function() {
        $('#myModal').foundation('reveal', 'close');
    },

    PostOnFacebook: function(text, username) {
        $.ajax({
            url: "/Social/FacebookPost",
            method: "POST",
            data: { text: text, username: username }
        }).success(function() {
            console.log("success");
            $('#myModal').foundation('reveal', 'open');
            setTimeout(SocialSpace.HideModal, 1500);
        });
    },

    SendTweet: function(text, username) {
        $.ajax({
            url: "/Social/SendTweet",
            method: "POST",
            data: { text: text, username: username }
        }).success(function () {
            console.log("success");
            $('#myModal').foundation('reveal', 'open');
            setTimeout(SocialSpace.HideModal, 1500);
        });
    }
};