@ModelType ProntoEntries.Entry

@Code
    ViewData("Title") = "New Entry"
End Code

<style>
    .backing {
        background: linear-gradient( rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5) ), url(@ViewBag.Background) no-repeat;
        height: 100%;
    }
    .logobox {
        content: url(@ViewBag.OrgImage);
        height: 100%;
        width: auto;
    }
</style>

<meta name="viewport" content="width=device-width" />
<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Enter Event - @ViewBag.RaceName</h2>
        @Html.ActionLink("Change Event", "Index", "RaceEvents")
    </div>
    <hr />

    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="NewEntryPage">
            <div class="RaceSelection">
                <div Class="form-group">
                    @*DISTANCE*@
                    <p style="text-align:center;">
                        Quick-Step Guide:<br /><br />
         
                        1. Select Distance in Dropdown Below<br />                        
                        2. Click on Create New<br />
                        3. Enter Participant Details<br />
                        4. You Will Be Redirected To Select The Required Options<br />
                        5. If Redirection Fails, Click on Enter Now Next To The Participant Name Below<br />
                        6. Participant Is Added To The Cart For Payment. Please Select View Cart
                    </p>
                    <div Class="col-md-10", id="divisionselect">
                        @Html.DropDownList("Distance", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="Select Distance")
                        @*@Html.EditorFor(Function(model) model.DivisionID, New With {.htmlAttributes = New With {.class = "form-control"}})*@
                        @Html.ValidationMessageFor(Function(model) model.DivisionID, "", New With {.class = "text-danger"})
                    </div>
                </div>
            </div>
            <div class="ParticipantSelection">
                <div>
                    @Html.Action("ViewParticipants", "Participants", New With {.Id = ViewBag.RaceID, .DivisionSelect = ViewBag.DivisionSelect})
                </div>
            </div>

            @*<div Class="form-group">
                <div Class="col-md-offset-2 col-md-10">
                    <input type="submit" value="Create" Class="btn btn-default" />
                </div>
            </div>*@
        </div>

    End Using
</div>


@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
<script>
        $("#divisionselect").change(function () {
            var did = $("#divisionselect option:selected").val();
            window.location.replace("?DivisionSelect=" + did);
        });

        if(@ViewBag.TotalEntries > 0)
            {
            alert("You have entered (" + @ViewBag.TotalEntries.ToString() + ") Participants.  Please view the cart for payment.");
        };
</script>
End Section


@*     <div Class="form-group">
    @Html.LabelFor(Function(model) model.ParticipantID, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div Class="col-md-10">
    @Html.EditorFor(Function(model) model.ParticipantID, New With {.htmlAttributes = New With {.class = "form-control"}})
    @Html.ValidationMessageFor(Function(model) model.ParticipantID, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div Class="form-group">
    @Html.LabelFor(Function(model) model.RaceID, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div Class="col-md-10">
    @Html.EditorFor(Function(model) model.RaceID, New With {.htmlAttributes = New With {.class = "form-control"}})
    @Html.ValidationMessageFor(Function(model) model.RaceID, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div Class="form-group">
    @Html.LabelFor(Function(model) model.DivisionID, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div Class="col-md-10">
    @Html.EditorFor(Function(model) model.DivisionID, New With {.htmlAttributes = New With {.class = "form-control"}})
    @Html.ValidationMessageFor(Function(model) model.DivisionID, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div Class="form-group">
    @Html.LabelFor(Function(model) model.Amount, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div Class="col-md-10">
    @Html.EditorFor(Function(model) model.Amount, New With {.htmlAttributes = New With {.class = "form-control"}})
    @Html.ValidationMessageFor(Function(model) model.Amount, "", New With {.class = "text-danger"})
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
    @Html.LabelFor(Function(model) model.PaymentReference, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div Class="col-md-10">
    @Html.EditorFor(Function(model) model.PaymentReference, New With {.htmlAttributes = New With {.class = "form-control"}})
    @Html.ValidationMessageFor(Function(model) model.PaymentReference, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div Class="form-group">
    @Html.LabelFor(Function(model) model.DistanceChange, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div Class="col-md-10">
                        <div Class="checkbox">
    @Html.EditorFor(Function(model) model.DistanceChange)
    @Html.ValidationMessageFor(Function(model) model.DistanceChange, "", New With {.class = "text-danger"})
                        </div>
                    </div>
                </div>

                <div Class="form-group">
    @Html.LabelFor(Function(model) model.ChangePaymentRef, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div Class="col-md-10">
    @Html.EditorFor(Function(model) model.ChangePaymentRef, New With {.htmlAttributes = New With {.class = "form-control"}})
    @Html.ValidationMessageFor(Function(model) model.ChangePaymentRef, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div Class="form-group">
    @Html.LabelFor(Function(model) model.TransferID, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div Class="col-md-10">
    @Html.EditorFor(Function(model) model.TransferID, New With {.htmlAttributes = New With {.class = "form-control"}})
    @Html.ValidationMessageFor(Function(model) model.TransferID, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div Class="form-group">
    @Html.LabelFor(Function(model) model.Result, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div Class="col-md-10">
    @Html.EditorFor(Function(model) model.Result, New With {.htmlAttributes = New With {.class = "form-control"}})
    @Html.ValidationMessageFor(Function(model) model.Result, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div Class="form-group">
                    <div Class="col-md-offset-2 col-md-10">
                        <input type = "submit" value="Create" Class="btn btn-default" />
                    </div>
                </div>
            </div>-->*@

