@ModelType ProntoEntries.Voucher

@Code
    ViewData("Title") = "Vouchers"
    Dim Total As Integer = 0
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Voucher</title>
</head>
<body>

    <div class="orgcontainer create">
        <div class="titlediv">
            <h2>Issue Voucher</h2>
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

        <form action="" method="get" style="text-align:center">
            <div class="DropdownDashboard">
                <div Class="DropdownDashboard item" , id="eventselect">
                    <p>Event Name</p>
                    @Html.DropDownList("RaceID", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="Select Event")
                </div>
                <input type="submit" name="submit" />
                @*<div class="DropdownSearches item">
                        <p>Search Value</p>
                        <input type="text" name="SearchValue" id="SearchValue" class="form-control" placeholder="@ViewBag.SearchText" />
                    </div>*@
            </div>
            <hr />
            <div Class="dashboardcontainer">
                @* ITEM SELECTION*@
                <div Class="reportitem">
                    <div Class="ReportContent">
                        <Table Class="table">
                            <tr>
                                <th>
                                    Item
                                </th>
                                <th>
                                    Qty
                                </th>
                                <th>
                                    Price
                                </th>
                                <th>
                                    Total
                                </th>
                            </tr>

                            @For Each item In ViewBag.RaceOptions
                                @Code
                                    Dim qtyd As Integer = If(IsNumeric(Request.Form("QtyD-" & item.DivisionID)), CInt(Request.Form("QtyD-" & item.DivisionID)), 0)
                                    Dim lineTotald As Decimal = qtyd * item.Price
                                End Code
                                @<tr>
                                    <td>
                                        @item.Distance
                                    </td>
                                    <td>
                                        <input type="text" id=QtyD-@item.DivisionID value="0" />
                                    </td>
                                    <td>
                                        <p id=AmountD-@item.DivisionID data-my-value=@item.Price>@item.Price</p>
</td>
                                    <td>
                                        <p id=TotalD-@item.DivisionID>@lineTotald.ToString()</p>
</td>
                                </tr>
                                    Next

                            @For Each item In ViewBag.ItemList
                                @Code
                                    Dim qtyi As Integer = If(IsNumeric(Request.Form("QtyI-" & item.OptionID)), CInt(Request.Form("QtyI-" & item.OptionID)), 0)
                                    Dim lineTotali As Decimal = qtyi * item.Amount
                                End Code
                                @<tr>
                                    <td>
                                        @item.Size
                                    </td>
                                    <td>
                                        <input type="text" id=QtyI-@item.OptionID value="@qtyi" min="0" />
                                    </td>
                                    <td>
                                        <p id=AmountI-@item.OptionID data-my-value=@item.Amount>@item.Amount</p>
</td>
                                    <td>
                                        <p id=TotalI-@item.OptionID>@lineTotali.ToString()</p>
                                    </td>
                                </tr>
                                    Next
                        </Table>
                    </div>
                </div>
            </div>

        </form>

        <hr />



        @If ViewBag.RaceSeleted IsNot Nothing Then


            @Using (Html.BeginForm())
                @Html.AntiForgeryToken()

                @<div>




                    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                    <div Class="form-group">
                        @Html.LabelFor(Function(model) model.Code, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        <div Class="col-md-10">
                            @Html.EditorFor(Function(model) model.Code, New With {.htmlAttributes = New With {.class = "form-control"}})
                            @Html.ValidationMessageFor(Function(model) model.Code, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div Class="form-group">
                        @Html.LabelFor(Function(model) model.Value, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        <div Class="col-md-10">
                            @Html.EditorFor(Function(model) model.Value, New With {.htmlAttributes = New With {.class = "form-control"}})
                            @Html.ValidationMessageFor(Function(model) model.Value, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div Class="form-group">
                        @Html.LabelFor(Function(model) model.IssuedBy, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        <div Class="col-md-10">
                            @Html.EditorFor(Function(model) model.IssuedBy, New With {.htmlAttributes = New With {.class = "form-control"}})
                            @Html.ValidationMessageFor(Function(model) model.IssuedBy, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div Class="form-group">
                        @Html.LabelFor(Function(model) model.Pf_Reference, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        <div Class="col-md-10">
                            @Html.EditorFor(Function(model) model.Pf_Reference, New With {.htmlAttributes = New With {.class = "form-control"}})
                            @Html.ValidationMessageFor(Function(model) model.Pf_Reference, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div Class="form-group">
                        @Html.LabelFor(Function(model) model.M_Reference, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        <div Class="col-md-10">
                            @Html.EditorFor(Function(model) model.M_Reference, New With {.htmlAttributes = New With {.class = "form-control"}})
                            @Html.ValidationMessageFor(Function(model) model.M_Reference, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div Class="form-group">
                        @Html.LabelFor(Function(model) model.Date, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        <div Class="col-md-10">
                            @Html.EditorFor(Function(model) model.Date, New With {.htmlAttributes = New With {.class = "form-control"}})
                            @Html.ValidationMessageFor(Function(model) model.Date, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div Class="form-group">
                        @Html.LabelFor(Function(model) model.Status, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        <div Class="col-md-10">
                            @Html.EditorFor(Function(model) model.Status, New With {.htmlAttributes = New With {.class = "form-control"}})
                            @Html.ValidationMessageFor(Function(model) model.Status, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div Class="form-group">
                        @Html.LabelFor(Function(model) model.UsedBy, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        <div Class="col-md-10">
                            @Html.EditorFor(Function(model) model.UsedBy, New With {.htmlAttributes = New With {.class = "form-control"}})
                            @Html.ValidationMessageFor(Function(model) model.UsedBy, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div Class="form-group">
                        @Html.LabelFor(Function(model) model.UsedDate, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        <div Class="col-md-10">
                            @Html.EditorFor(Function(model) model.UsedDate, New With {.htmlAttributes = New With {.class = "form-control"}})
                            @Html.ValidationMessageFor(Function(model) model.UsedDate, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div Class="form-group">
                        @Html.LabelFor(Function(model) model.UsedM_Reference, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        <div Class="col-md-10">
                            @Html.EditorFor(Function(model) model.UsedM_Reference, New With {.htmlAttributes = New With {.class = "form-control"}})
                            @Html.ValidationMessageFor(Function(model) model.UsedM_Reference, "", New With {.class = "text-danger"})
                        </div>
                    </div>

                    <div Class="form-group">
                        <div Class="col-md-offset-2 col-md-10">
                            <input type="submit" value="Create" Class="btn btn-default" />
                        </div>
                    </div>
                </div>

            End Using
        End If
    </div>
    <div>
        @Html.ActionLink("Back to List", "Index")
    </div>
</body>
</html>



@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
<script>
        $("[id^=Qty]").change(function () {


            var QtyID = $(this).prop('id');
            var cut = QtyID.length - 5;
            var TotalID = QtyID.slice(-cut);
            var MainTotal = parseFloat(document.getElementById("Value").value);

            TotalID = "Total" + QtyID.charAt(3) + "-" + TotalID;
            var PriceID = "Amount" + QtyID.charAt(3) + "-" + QtyID.slice(-cut);
            var Total = parseFloat(document.getElementById(TotalID).value)

            if (isNaN(Total)) {
                alert(Total);
                Total = document.getElementById(QtyID).value * document.getElementById(PriceID).innerHTML - 0;
                alert(Total);
            } else {
                MainTotal = MainTotal - Total;
                Total = document.getElementById(QtyID).value * document.getElementById(PriceID).innerHTML - parseFloat(document.getElementById(TotalID).value);
                
                alert("twee");
            }

            
            let linetotal = document.getElementById(TotalID);

            
            linetotal.innerHTML = parseFloat(Total.toString());

            

            if (isNaN(MainTotal)) {
                MainTotal = 0 + parseFloat(Total);
                alert("drie");
            } else {
                MainTotal = MainTotal + parseFloat(Total);
                alert("vier");
            }

            
            alert(MainTotal);

            document.getElementById("Value").value = parseFloat(MainTotal);

            //$row.find(".item-total").text((quantity * price).toLocaleString('en-US', { style: 'currency', currency: 'USD' }));
        });


</script>
End Section

