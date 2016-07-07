var SocialSpace = {
    HideModal: function() {
        //$('#myModal').foundation('reveal', 'close');
    },

    PostOnFacebook: function(text, username, imagePath) {
        $.ajax({
            url: "/Social/FacebookPost",
            method: "POST",
            data: { text: text, username: username, imagePath: imagePath}
        }).success(function() {
            console.log("success");
            $('#genericModal #title').html("Info");
            $('#genericModal #modalBody').html("Published to Facebook feed successfully!");
            $('#genericModal').modal('show');
        });
    },

    SendTweet: function(text, username, imagePath) {
        $.ajax({
            url: "/Social/SendTweet",
            method: "POST",
            data: { text: text, username: username, imagePath: imagePath}
        }).success(function () {
            console.log("success");
            $('#genericModal #title').html("Info");
            $('#genericModal #modalBody').html("Published to Twitter feed successfully!");
            $('#genericModal').modal('show');
        });
    },

    Scheduled: function () {
        console.log("success");
        $('#genericModal #title').html("Info");
        $('#genericModal #modalBody').html("Job scheduled with success!");
        $('#genericModal').modal('show');
    },

    OnBegin: function (button) {
        $(button).prop('disabled', true);
    },

    OnSuccess: function (data) {
        console.log(data);
        if (data["status"] === 0) {
            $('#genericModal #title').html("Error");
            $('#genericModal #title').css("color", "red");
            $('#genericModal #modalBody').html(data["message"]);
            $('#genericModal').modal('show');
        }
        if (data["status"] === 1) {
            window.location.href = data["url"];
        }
    },

    DeleteJob: function(id) {
        $.ajax({
            url: "/Social/DeleteJob",
            method: "POST",
            data: { jobId: id }
        }).success(function (data) {
            location.reload();
        });
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