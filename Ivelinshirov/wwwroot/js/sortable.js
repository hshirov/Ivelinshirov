$(function () {
    $("#sortable").sortable({
        update: function () {
            $(".save-order-button").css({ "visibility": "visible", "margin-bottom": "15px", "margin-top": "15px" });
        },
        containment: ".sortable-grid",
        tolerance: "pointer"
    });
    $("#sortable").disableSelection();

    $(".save-order-button").click(function () {
        var result = $('#sortable').sortable("toArray", { attribute: 'id' });

        $.post("/Admin/UpdateOrder", { Ids: result }, function (data) {
            location.reload();
        });
    });
});