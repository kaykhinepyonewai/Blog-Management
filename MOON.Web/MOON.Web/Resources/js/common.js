$(document).ready(function () {
    try {
        $(window).on("load", function () {
            $(".loader").fadeOut(500, function () {
                $(this).hide();
            })
        })
        //Trend Slide
        $(".article-slider").slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            infinite: true,
            dots: false,
            arrows: true,
            variableWidth: false,
            cssEase: "linear",
            responsive: [
                {
                    breakpoint: 1160,
                    settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1,
                        infinite: true,
                        variableWidth: true,
                        dots: false,
                    },
                },
                {
                    breakpoint: 767,
                    settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1,
                        infinite: true,
                        variableWidth: false,
                        dots: false,
                    },
                },
            ],
        });
        //Trend Slide
        $(".trend-slide").slick({
            slidesToShow: 3,
            slidesToScroll: 1,
            infinite: true,
            dots: false,
            arrows: false,
            autoplay: true,
            variableWidth: true,
            cssEase: "linear",
            responsive: [
                {
                    breakpoint: 1160,
                    settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1,
                        infinite: true,
                        variableWidth: true,
                        dots: false,
                    },
                },
                {
                    breakpoint: 767,
                    settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1,
                        infinite: true,
                        variableWidth: true,
                        dots: false,
                    },
                },
            ],
        });
        //font size check heightline
        $(".slick-track>.slick-slide").heightLine({
            fontSizeCheck: true,
        });
        $(".article-list > .article-item ").heightLine({
            fontSizeCheck: true,
        })

        $('#txtSearch').on('change', function (e) {
            console.log($('#btnSearch').on("Submit").click());
        });
    } catch (e) {
        console.warn(e.message);
    }


    try {
        //font size check heightline
        $(".slick-track>.slick-slide").heightLine({
            fontSizeCheck: true,
        });

        $(".closeReaction").click(function (e) {
            e.preventDefault();
            if (e.keycode != 13) {
                const closeBtn = $("#viewerClose" + x);
                const viewer = $("#viewers" + x);
                viewer.toggleClass("active");
                closeBtn.removeClass("active");
            }
        })

        $('[id*=txtDescription]').summernote({
            tabsize: 2,
            height: 100
        });
    } catch (e) {
        console.warn(e.message);
    }
    $(".show-btn").click(function (e) {
        e.preventDefault();
        $(".d-aside").animate({ marginLeft: 0 });
        $(".d-aside").toggleClass("active");
        $("body").toggleClass("hide");
    });
    $(".nav-close").click(function (e) {
        e.preventDefault();
        $(".d-aside").animate({ marginLeft: "-100%" });
        $(".d-aside").toggleClass("active");
        $("body").toggleClass("hide");
    })


    //Quote generator
    const getQuote = async () => {
        const data = await fetch(`https://api.quotable.io/random`);
        const result = await data.json();
        console.log(result);
        $(".quote-wrapper .quote-describe").text(result.content);
    }
    getQuote();
});

$(".viewerBtn").click(function (e) {
    e.preventDefault();
    if (e.keycode != 13) {
        const viewer = $(".viewers");
        const close = $(".viewerClose");
        viewer.toggleClass("active");
        close.addClass("active");
    }
})

$(".closeReaction").click(function (e) {
    e.preventDefault();
    const closeBtn = $(".viewerClose");
    const viewer = $(".viewers");
    viewer.toggleClass("active");
    closeBtn.removeClass("active");
})


$(".commentBtn").click(function (e) {
    e.preventDefault();
    $(".comment-asidenav").toggleClass("active");
})


$(".comment-aside-close").click(function (e) {
    e.preventDefault();
    $(".comment-asidenav").toggleClass("active");
})

$(".close-com-btn").click(function (e) {
    e.preventDefault();
    $(".comment-asidenav").toggleClass("active");
})

$("#txtMainSearch").on("change", function (e) {
    e.preventDefault();
    console.log(e);
})

window.delHandle = function (x, y = null) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this data!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                swal("Poof! Your data has been deleted!", {
                    icon: "success",
                });
                $("[id*=hdnValueId]").val(x);
                $("[id*=hdnArticleId]").val(y);
                setTimeout(() => {
                    $("[id*=btnDeleteHandler]").click();
                }, 800);
            } else {
                swal("Your data is safe!");
            }
        });
}



window.btnReject = function (e) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this data!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                swal("Poof! Your data has been deleted!", {
                    icon: "success",
                });
                $("[id*=btnRejectHandler]").click();
                let inputName = $("[id*=btnReject]").val();
               
                if (inputName === "Reject") {
                    setTimeout(() => {
                        Toast();
                    }, 2100)
                } else {
                    setTimeout(() => {
                        console.log("Current article report has been completely removed")
                    },1800)
                }
            } else {
                swal("Your data is safe!");
            }
        });
}

window.Toast = function () {
    swal("Reject Mail!", "A rejection email has been successfully sent to the owner of this article.");
}

$('[id*=txtTitle]').on('focus', function () {
    $('[id*=lblTitle]').html('');
    $('[id*=lblTitle]').hide();
});

$('[id*=txtSlug]').on('focus', function () {
    $('[id*=lblSlug]').html('');
    $('[id*=lblSlug]').hide();
});

$('[id*=txtDescription]').on('focus', function () {
    $('[id*=revDescription]').html('');
    $('[id*=revDescription]').hide();
});

$('[id*=FileUploadThum]').on('focus', function () {
    $('[id*=lblThubmail]').html('');
    $('[id*=lblThubmail]').hide();
});

$('[id*=FileUploadPhoto]').on('focus', function () {
    $('[id*=lblPhoto]').html('');
    $('[id*=lblPhoto]').hide();
});

$("[id*=txtRpMessage]").on('focus', function () {
    $('[id*=lblErrorMsg]').html('');
    $('[id*=lblErrorMsg]').hide();
})

$("[id*=txtEmail]").on("focus", function () {
    $('[id*=lblErrorMsg]').html('');
    $('[id*=lblErrorMsg]').hide();
})

$("[id*=txtPassword]").on("focus", function () {
    $('[id*=lblErrorMsg]').html('');
    $('[id*=lblErrorMsg]').hide();
    $("[id*=lblCheckPassword]").html("");
    $("[id*=lblCheckPassword]").hide();
})

$("[id*=txtConfirmPassword]").on("focus", function () {
    $('[id*=lblErrorMsg]').html('');
    $('[id*=lblErrorMsg]').hide();
    $("[id*=lblCheckPassword]").html("");
    $("[id*=lblCheckPassword]").hide();
})