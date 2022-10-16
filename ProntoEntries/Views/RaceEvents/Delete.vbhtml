@ModelType ProntoEntries.RaceEvent
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>RaceEvent</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.RaceName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.RaceName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.RaceDescription)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.RaceDescription)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.RaceDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.RaceDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.RaceType)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.RaceType)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Coordinates)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Coordinates)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Address)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Address)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.City)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.City)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Province)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Province)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.AdminCharge)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.AdminCharge)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.OrgID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OrgID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Image)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Image)
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
