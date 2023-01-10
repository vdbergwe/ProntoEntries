@ModelType ProntoEntries.Sale

@Code
    ViewData("Title") = "Update"
End Code



<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Update Entry Option</title>
</head>
<body>

    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div Class="orgcontainer partcreate">
            <div Class="titlediv">
                <h2> Update Entry Option - @Html.Action("Get_ParticipantName", New With {.Id = ViewBag.ParticipantID})</h2>
                @Html.ActionLink("Back to List", "Index")
            </div>
            <hr />


            <div class="form-group">
                @Html.HiddenFor(Function(model) model.SaleID, htmlAttributes:=New With {.class = "control-label col-md-2"})
            </div>

            <div class="form-group">
                @Html.HiddenFor(Function(model) model.RaceID, htmlAttributes:=New With {.class = "control-label col-md-2"})
            </div>

            <div class="form-group">
                @Html.HiddenFor(Function(model) model.DivisionID, htmlAttributes:=New With {.class = "control-label col-md-2"})
            </div>

            <div class="participantright">
                <div class="partelement" style="color:white;">
                    Item Name
                </div>
                <div class="partelement" style="color:dodgerblue;">
                    @Html.Action("Get_ItemName", New With {.Id = Model.ItemID, .htmlAttributes = New With {.class = "control-label col-md-2 labelfix"}})
                    @Html.HiddenFor(Function(model) model.ItemID, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.ItemID, "", New With {.class = "text-danger"})
                </div>
                <div class="partelement" style="color:white;">
                    Select Option
                </div>
                <div class="partelement">
                    @Html.DropDownList("OptionID", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="")
                    @*@Html.EditorFor(Function(model) model.OptionID, New With {.htmlAttributes = New With {.class = "form-control"}})*@
                    @Html.ValidationMessageFor(Function(model) model.OptionID, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.HiddenFor(Function(model) model.UserID, htmlAttributes:=New With {.class = "control-label col-md-2"})
            </div>

            <div class="form-group">
                @Html.HiddenFor(Function(model) model.Indemnity, htmlAttributes:=New With {.class = "control-label col-md-2"})
            </div>

            <div class="form-group">
                @Html.HiddenFor(Function(model) model.TandC, htmlAttributes:=New With {.class = "control-label col-md-2"})
            </div>

            <div class="form-group">
                @Html.HiddenFor(Function(model) model.ParticipantID, htmlAttributes:=New With {.class = "control-label col-md-2"})
            </div>

            <div class="form-group">
                @Html.HiddenFor(Function(model) model.M_reference, htmlAttributes:=New With {.class = "control-label col-md-2"})
            </div>

            <div class="form-group">
                @Html.HiddenFor(Function(model) model.Pf_reference, htmlAttributes:=New With {.class = "control-label col-md-2"})
            </div>

            <div class="form-group">
                @Html.HiddenFor(Function(model) model.Verified, htmlAttributes:=New With {.class = "control-label col-md-2"})
            </div>

            <div class="form-group">
                @Html.HiddenFor(Function(model) model.SaleDate, htmlAttributes:=New With {.class = "control-label col-md-2"})
            </div>

            <div class="button-group">
                <p></p>
                <input type="submit" value="Update" class="btn btn-default" />
            </div>

            @*<div Class="ReportContent">
                <Table Class="table">
                    <tr>
                        <th>
                            SaleID
                        </th>
                        <th>
                            Option Name
                        </th>
                        <th>
                            Current Value
                        </th>

                    </tr>

                    <tr>
                        <td>
                            @Html.DisplayFor(Function(modelItem) Model.SaleID)
                        </td>
                        <td>
                            @Html.Action("Get_ItemName", New With {.Id = Model.ItemID})
                        </td>
                        <td>
                            @Html.Action("Get_ItemChosen", New With {.Id = Model.OptionID})
                        </td>
                    </tr>

                </Table>
            </div>*@


        </div>

    End Using
</body>
</html>