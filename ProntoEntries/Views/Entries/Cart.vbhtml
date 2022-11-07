@ModelType IEnumerable(Of ProntoEntries.Entry)

@Code
    ViewData("Title") = "Cart"
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <p>
        @*@Html.ActionLink("Add Entry", "NewEntry")*@
    </p>
    <table class="table">
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.ParticipantID)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.RaceID)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.DivisionID)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Amount)
            </th>
            <th></th>
        </tr>

        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.ParticipantID)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.RaceID)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.DivisionID)
                </td>
                <td>
                    R
                    @Html.DisplayFor(Function(modelItem) item.Amount)
                </td>
                <td>
                    @*@Html.ActionLink("Edit", "Edit", New With {.id = item.EntryID}) |
                        @Html.ActionLink("Details", "Details", New With {.id = item.EntryID}) |*@
                    @Html.ActionLink("Remove", "Delete", New With {.id = item.EntryID})
                </td>
            </tr>
        Next


    </table>
    <div>
        Total: R
        @ViewBag.Total
    </div>

    @Html.ActionLink("Payment", "SubmitToPayfast", "Entries")

    @*<div Class="button-group">
            <button id="btnPay" class="btn btn-default" onclick="addBooking();">Pay for Entries</button>
        </div>*@

    @*<div Class="RaceControlBanner EntryLink">
            @Html.ActionLink("Pay", "ToPayfast", "Entries", New With {.id = ViewBag.PaymentReference}, New With {.class = "btnEntryLink"})
        </div>*@

    @*<div>
        <form id="payFastForm" action="https://sandbox.payfast.co.za/eng/process" method="post">
            <input type="hidden" name="merchant_id" value=@ViewBag.MerchantID>
            <input type="hidden" name="merchant_key" value=@ViewBag.Merchant_key>
            <input type="hidden" name="return_url" value=@ViewBag.ReturnURL>
            <input type="hidden" name="cancel_url" value=@ViewBag.CancelURL>
            <input type="hidden" name="notify_url" value=@ViewBag.NotifyURL>
            <input type="hidden" name="m_payment_id" value=@ViewBag.PaymentID>
            <input type="hidden" name="amount" value=@ViewBag.Amount>
            <input type="hidden" name="item_name" value=@ViewBag.item_name>
            <input type="hidden" name="confirmation_address" value=@ViewBag.EmailAddress>
            <input type="hidden" name="signature" value=@ViewBag.Signature>
    <meta name="signature" value=@ViewBag.Signature id="signature" />
    </form>
    </div>*@
</body>
</html>

<script>
    //function addBooking() {
    //    document.getElementById("payFastForm").submit();
    //}
</script>
