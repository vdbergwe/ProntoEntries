@ModelType ProntoEntries.Sale
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>Sale</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.RaceID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.RaceID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DivisionID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DivisionID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ItemID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ItemID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UserID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UserID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Indemnity)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Indemnity)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.TandC)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.TandC)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ParticipantID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ParticipantID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.M_reference)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.M_reference)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Pf_reference)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Pf_reference)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.OptionID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OptionID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Verified)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Verified)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
