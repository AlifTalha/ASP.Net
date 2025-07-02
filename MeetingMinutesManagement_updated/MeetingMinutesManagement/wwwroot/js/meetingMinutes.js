$(document).ready(function () {
    // Handle customer type change
    $('input[name="CustomerType"]').change(function () {
        var customerType = $(this).val();
        $.get('/MeetingMinutes/GetCustomers', { customerType: customerType }, function (data) {
            var customerDropdown = $('#CustomerId');
            customerDropdown.empty();
            customerDropdown.append($('<option/>').val('').text('-- Select Customer --'));

            $.each(data, function (index, item) {
                customerDropdown.append($('<option/>').val(item.id).text(item.name));
            });
        });
    });

    // Add product to table
    $('#addProduct').click(function () {
        var productId = $('#ProductServiceId').val();
        var productName = $('#ProductServiceId option:selected').text();
        var quantity = $('#Quantity').val();
        var unit = $('#ProductServiceId option:selected').data('unit');

        if (productId && quantity > 0) {
            var row = `<tr data-product-id="${productId}">
                <td>${productName}</td>
                <td>${quantity}</td>
                <td>${unit}</td>
                <td>
                    <button type="button" class="btn btn-danger btn-sm remove-product">Remove</button>
                    <input type="hidden" name="productServiceIds" value="${productId}" />
                    <input type="hidden" name="quantities" value="${quantity}" />
                </td>
            </tr>`;

            $('#productTable tbody').append(row);
            $('#ProductServiceId').val('');
            $('#Quantity').val('1');
        }
    });

    // Remove product from table
    $('#productTable').on('click', '.remove-product', function () {
        $(this).closest('tr').remove();
    });

    // Form submission
    $('#meetingMinutesForm').submit(function (e) {
        if ($('#productTable tbody tr').length === 0) {
            e.preventDefault();
            alert('Please add at least one product/service');
        }
    });
});