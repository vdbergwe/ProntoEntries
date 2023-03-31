@ModelType IEnumerable(Of ProntoEntries.Sale)

@Code
    ViewData("Title") = "Cart"
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
    <script>
        function updateConverted() {
            console.log("Function called");
            document.getElementById("VoucherLink").innerHTML = '<a href="/Entries/Cart?Voucher=' + document.getElementById($(this).prop('id')).value + '">Apply Voucher</a>';
        });
    </script>
</head>
<body>
    <div class="orgcontainer create">
        <div class="titlediv">
            <h2>Cart</h2>
            <ul>
                <li>
                    <input type="text" id="VoucherCode" onkeyup="updateConverted()" value=@ViewBag.VoucherCode />
                </li>
                <li id="VoucherLink">
                    @Html.ActionLink("Apply Voucher", "Cart", "Entries", New With {.Voucher = ViewBag.VoucherCode}, Nothing)
                </li>
            </ul>
        </div>



        <hr />

        @If Model.Count > 0 Then

            @<div>

    <Table Class="table">

        @For Each Ditem In ViewBag.UniqueP
            @<tr style="justify-content:center">
                <td style="color: dodgerblue">
                    @Html.Action("Get_ParticipantName", New With {.Id = (Ditem)})
                </td>
            </tr>
            @<tr>
                <th>
                    Event
                </th>
                <th>
                    Distance
                </th>
                <th>
                    Add-On
                </th>
                <th>
                    Amount
                </th>
                <th></th>
            </tr>
            @For Each item In Model
                @If item.ParticipantID = (Ditem) Then
                    @<tr>
                        <td>
                            @Html.Action("Get_RaceName", New With {.Id = item.RaceID})
                        </td>
                        <td>
                            @Html.Action("Get_DivisionDistance", New With {.Id = item.DivisionID})
                        </td>
                        <td>
                            @If item.RaceID Is Nothing Then
                                @<div>
                                    @Html.Action("Get_ItemName", "AddonOptions", New With {.Id = item.ItemID}) ->
                                    @Html.Action("Get_ItemSize", "AddonOptions", New With {.Id = item.OptionID})
                                </div>

                            End If
                        </td>
                        <td>
                            @Html.Action("Get_CartAmount", New With {.Id = item.DivisionID, .OptionID = item.OptionID})
                        </td>
                        <td>
                            @*@Html.ActionLink("Edit", "Edit", New With {.id = item.EntryID}) |
                                @Html.ActionLink("Details", "Details", New With {.id = item.EntryID}) |*@
                            @If item.RaceID IsNot Nothing Or item.ItemID = 1 Then
                                @Html.ActionLink("Remove", "Delete", "Sales", New With {.id = item.SaleID}, Nothing)
                            Else
                                @*@Html.Action("Get_ItemSize", "AddonOptions", New With {.Id = item.ItemID})*@
                            End If
                        </td>
                    </tr>
                End If

            Next
        Next


        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td>
                <hr />
            </td>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td> Admin Fee :      </td>
            <td>
                R
                @ViewBag.AdminCharge
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td> Total :      </td>
            <td>
                R
                @ViewBag.Total
            </td>
        </tr>
        @If ViewBag.VoucherValid = True Then
            @<tr>
                <td></td>
                <td></td>
                <td></td>
                <td> Voucher :            </td>
                <td>
                    -R
                    @ViewBag.VoucherTotal
                </td>
            </tr>
            @<tr>
                <td></td>
                <td></td>
                <td></td>
                <td> Outstanding :             </td>
                <td>
                    R
                    @ViewBag.Outstanding
                </td>
            </tr>
        End If


    </Table>

    @If ViewBag.Outstanding = 0 Then
        @<div Class="button-cart">
            <div Class="linkbutton cart">
                @Html.ActionLink("Complete Order", "VoucherPayment", "Entries", New With {.Voucher = ViewBag.VoucherCode}, Nothing)
            </div>
        </div>
    Else
        @<div Class="button-cart">
            <div Class="linkbutton cart">
                @Html.ActionLink("Payment", "SubmitToPayfast", "Entries", New With {.Voucher = ViewBag.VoucherCode}, Nothing)
            </div>
        </div>
    End If


</div>
        Else
            @<div style="text-align:center">
                <p>No Items In Cart</p>
            </div>
        End If


    </div>

</body>
</html>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
    <script>
        function updateConverted() {
            console.log("Function called");

            // Get the input element
            var input = document.getElementById("VoucherCode");

            // Get the current value of the input
            var inputValue = input.value;

            // Do some conversion with the input value
            var convertedValue = inputValue.toUpperCase();

            // Update the output element with the converted value
            if (convertedValue == "") {
                document.getElementById("VoucherLink").innerHTML = '<a href="/Entries/Cart">Apply Voucher</a>';
            } else {
                document.getElementById("VoucherLink").innerHTML = '<a href="/Entries/Cart?Voucher=' + convertedValue + '">Apply Voucher</a>';
            }


        }
    </script>
End Section