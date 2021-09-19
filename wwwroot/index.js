$(document).ready(function () {
    $('#submitbutton').on('click',async function (e) {
        e.preventDefault();

        console.log($('#shortenform').serialize());

        let req = {
            "url" : $("#url").val(),
            "alias" : $("#alias").val()
        }
        console.log(req);

        $.ajax({
                type: "post",
                url: "api/URL/Shorten/",
                data: req,
                contentType: "application/json; charset=utf-8", 
                success: function (response) {
                    console.log(response)                   
                    }
                });
    });
});