@ModelType ProntoEntries.Participant
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Participant</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.FirstName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.FirstName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.MiddleNames)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.MiddleNames)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.LastName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.LastName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.IDNumber)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.IDNumber)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Day)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Day)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Month)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Month)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Year)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Year)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.RaceNumber)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.RaceNumber)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.EmailAddress)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.EmailAddress)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.MedicalName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.MedicalName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.MedicalNumber)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.MedicalNumber)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.EmergencyContact)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.EmergencyContact)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.EmergencyNumber)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.EmergencyNumber)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BoodType)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BoodType)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Allergies)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Allergies)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.AdditionalInfo)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.AdditionalInfo)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DoctorName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DoctorName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DoctorContact)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DoctorContact)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Clubname)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Clubname)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Country)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Country)
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
            @Html.DisplayNameFor(Function(model) model.UserID)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UserID)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.EventMailer)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.EventMailer)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Offers)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Offers)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.ParticipantID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
