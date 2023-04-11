@ModelType ProntoEntries.Entry

@Code
    ViewData("Title") = "Upgrade"
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Upgrade / Downgrade</title>
</head>
<body>


    <div class="orgcontainer create">
        <div class="titlediv">
            <h2>Change Distance</h2>
            @Html.ActionLink("Back to Entries", "Index", "Entries")

        </div>
        <hr />

        <div style="text-align:center">
            <div style="align-items:center">
                <p style="color:white;">
                    Upgrade / Downgrade Distance For Entry ID: @Html.DisplayFor(Function(model) model.EntryID)
                    <br />
                    <br />
                    Your current distance selection: @ViewBag.CurrentDistance KM
                    <br />
                    <br />
                    NOTE: ONLY ONE DISTANCE CHANGE PERMITTED
                    <div Class="col-md-10" , id="divisionselect">
                        @Html.DropDownList("Distance", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="Select Distance")
                        @*@Html.EditorFor(Function(model) model.DivisionID, New With {.htmlAttributes = New With {.class = "form-control"}})*@
                        @Html.ValidationMessageFor(Function(model) model.DivisionID, "", New With {.class = "text-danger"})
                    </div>
                </p>
            </div>

            <div class="linkbutton">
                @Html.ActionLink("Change Distance", "PerformUpgrade", "Entries", New With {.id = Model.EntryID, .DivisionSelect = ViewBag.DivisionSelect}, Nothing)
            </div>


        </div>
    </div>

</body>
</html>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
    <script>
        $("#divisionselect").change(function () {
            var did = $("#divisionselect option:selected").val();
            window.location.replace("?DivisionSelect=" + did);
        });

    </script>
End Section