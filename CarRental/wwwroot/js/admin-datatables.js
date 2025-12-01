// Ensure DOM is ready
$(document).ready(function () {
    $('.datatable').DataTable({
        dom: 'Bfrtip', // Buttons layout
        buttons: [
            {
                extend: "excel",
                className: "btn btn-sm btn-success mx-1"
            },
            {
                extend: "csv",
                className: "btn btn-sm btn-info mx-1"
            },
            {
                extend: "pdf",
                className: "btn btn-sm btn-danger mx-1"
            },
            {
                extend: "print",
                className: "btn btn-sm btn-primary mx-1"
            }
        ],
        paging: true,
        searching: true,
        ordering: true
    });
});
