@ModelType ProntoEntries.Sale
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

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
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.SaleID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
