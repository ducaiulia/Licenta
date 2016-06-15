var SocialSpace = {
    HideModal: function() {
        $('#myModal').foundation('reveal', 'close');
    },

    PostOnFacebook: function(text, username, imagePath) {
        $.ajax({
            url: "/Social/FacebookPost",
            method: "POST",
            data: { text: text, username: username, imagePath: imagePath}
        }).success(function() {
            console.log("success");
            $('#myModal #genericText').html("Published to Facebook feed successfully!");
            $('#myModal').foundation('reveal', 'open');
            setTimeout(SocialSpace.HideModal, 1500);
        });
    },

    SendTweet: function(text, username, imagePath) {
        $.ajax({
            url: "/Social/SendTweet",
            method: "POST",
            data: { text: text, username: username, imagePath: imagePath}
        }).success(function () {
            console.log("success");
            $('#myModal #genericText').html("Published to Twitter feed successfully!");
            $('#myModal').foundation('reveal', 'open');
            setTimeout(SocialSpace.HideModal, 1500);
        });
    },

    Scheduled: function () {
        console.log("success");
        $('#myModal #genericText').html("Job scheduled with success!");
        $('#myModal').foundation('reveal', 'open');
        setTimeout(SocialSpace.HideModal, 1500);
    },

    OnSuccess: function (data) {
        console.log(data);
        if (data["status"] === 0) {
            $('#myModal #modalTitle').html("Error");
            $('#myModal #modalTitle').css("color", "red");
            $('#myModal #genericText').html(data["message"]);
            $('#myModal').foundation('reveal', 'open');
            setTimeout(SocialSpace.HideModal, 2000);
        }
        if (data["status"] === 1) {
            window.location.href = data["url"];
        }
    }
    //ScheduleJob: function(text, username, imagePath, datetime) {
    //    $.ajax({
    //        url: "/Social/ScheduleJob",
    //        method: "POST",
    //        data: { text: text, username: username, imagePath: imagePath, toRun: datetime}
    //    }).success(function () {
    //        console.log("success");
    //        $('#myModal').foundation('reveal', 'open');
    //        setTimeout(SocialSpace.HideModal, 1500);
    //    });
    //}
};