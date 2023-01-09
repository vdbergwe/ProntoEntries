@ModelType ProntoEntries.Participant
@Code
    ViewData("Title") = "Delete"
End Code


<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Delete Participant</h2>
        @Html.ActionLink("Back to Participants", "Index", "Participants")

    </div>
    <hr />

    <div style="text-align:center">
        <div style="align-items:center">
            <p style="color:white;">
                Deleting @Html.DisplayFor(Function(model) model.FirstName) @Html.DisplayFor(Function(model) model.LastName) (@Html.DisplayFor(Function(model) model.IDNumber))
            </p>
        </div>

        @Using (Html.BeginForm())
            @Html.AntiForgeryToken()

            @<div class="form-actions no-color" style="align-items:center">
                <input type="submit" value="Delete" class="btn btn-default" />
            </div>
        End Using


    </div>
</div>