@ModelType IEnumerable(Of ProntoEntries.Voucher)

@Code
    ViewData("Title") = "Vouchers"
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>

    <div class="orgcontainer create">
        <div class="titlediv">
            <h2>Voucher List</h2>
            <ul>
                <li>
                    @Html.ActionLink("Create New", "Create")
                </li>
            </ul>
            @*@If ViewBag.SelectedRace IsNot Nothing Then
                    @<div>
                        <ul>
                            <li>
                                @Html.ActionLink("Export Race Detail", "ExporttoExcelRaceDetail", New With {.id = ViewBag.SelectedRace})
                            </li>
                            <li>
                                @Html.ActionLink("Race Detail With Add-ons", "ExporttoExcelAddonSales", New With {.id = ViewBag.SelectedRace})
                            </li>
                        </ul>
                    </div>
                End If*@
        </div>

        <hr />

        <table class="table">
            <tr>
                <th>
                    @Html.DisplayNameFor(Function(model) model.Code)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.Value)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.IssuedBy)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.Pf_Reference)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.M_Reference)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.Date)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.Status)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.UsedBy)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.UsedDate)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.UsedM_Reference)
                </th>
                <th></th>
            </tr>

            @For Each item In Model
                @<tr>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Code)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Value)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.IssuedBy)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Pf_Reference)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.M_Reference)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Date)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Status)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.UsedBy)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.UsedDate)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.UsedM_Reference)
                    </td>
                    @If item.Status = "Active" Then
                        @<td>
                            @Html.ActionLink("Revoke", "Delete", New With {.id = item.VoucherID})
                        </td>
                    End If


                </tr>
            Next

        </table>
    </div>


</body>
</html>
