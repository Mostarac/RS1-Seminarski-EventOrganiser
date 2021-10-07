// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $("#content-slider").lightSlider({
        loop: true,
        keyPress: true,
        speed: 400, //ms'
        auto: true,
        slideEndAnimation: true
    });

    $("#contact-button").click(function (e) {
        $("#message").val("");
        openModal();
    });

    $("#submit-contact-form").click(function (e) {
        closeModal();
    });

    $("#contact-form").on('submit', function (event) {
        event.preventDefault();
        
        $.post("/Events/SendMessage", { id: $('#eventId').val(), message: $('#message').val() }, function (result) {
            console.log("Success");
        });
    });

    $("#rating-form").on('submit', function (event) {
        event.preventDefault();

        var rating = document.querySelector('input[name="Rating"]:checked').value;
        
        $.post("/Events/EventRating", { id: $('#eventId').val(), rating: rating }, function (result) {
            console.log("Success");
        });
    });
});

function openModal() {
    $("#messageModal").css("display", "block");
}

function closeModal() {
    $("#messageModal").css("display", "none");
}

$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('[data-toggle="popover"]').popover({
        placement: 'bottom',
        content: function () {
            return $("#notification-content").html();
        },
        html: true
    });

    $('body').append('<div id="notification-content" class="hide"></div>');

    function getNotification() {
        var res = "<ul class='list-group'>";
        $.ajax({
            url: "/Notifications/GetNotification",
            method: "GET",
            success: function (result) {

                if (result.count !== 0) {
                    $("#notificationCount").html(result.count);
                    $("#notificationCount").show('slow');
                } else {
                    $("#notificationCount").html();
                    $("#notificationCount").hide('slow');
                    $("#notificationCount").popover('hide');
                }

                var notifications = result.userNotification;
                notifications.forEach(element => {
                    res = res + "<li class='list-group-item notification-text' data-id='" + element.notification.id + "'>" + element.notification.text + "</li>";
                });

                res = res + "</ul>";

                $("#notification-content").html(res);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    $("ul").on('click',
        'li.notification-text',
        function (e) {
            var target = e.target;
            var id = $(target).data('id');

            readNotification(id, target);
        });

    function readNotification(id, target) {
        $.ajax({
            url: "/Notifications/ReadNotification",
            method: "GET",
            data: { notificationId: id },
            success: function (result) {
                getNotification();
                $(target).fadeOut('slow');
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function playSound() {
        var thisSound = document.getElementById("sound");
        thisSound.play();
    }

    getNotification();

    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/signalServer")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("displayNotification", (e) => {       
        getNotification();
        playSound();
    });

    connection.start();

});
