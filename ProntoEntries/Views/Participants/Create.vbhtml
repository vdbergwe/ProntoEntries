@ModelType ProntoEntries.Participant
@Code
    ViewData("Title") = "New Participant"
End Code

<div class="orgcontainer partcreate">
    <div class="titlediv">
        <h2>New Participant</h2>
        @Html.ActionLink("Back to List", "Index")
    </div>
    <hr />


    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div>
            @*<h4>Participant</h4>*@
            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
            <div class="partelement">
                    @Html.LabelFor(Function(model) model.FirstName, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.FirstName, New With {.htmlAttributes = New With {.class = "form-control"}})                         
                    @Html.ValidationMessageFor(Function(model) model.FirstName, "", New With {.class = "text-danger"})       
            </div>

            <div class="partelement">                
                    @Html.LabelFor(Function(model) model.MiddleNames, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.MiddleNames, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.MiddleNames, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.LastName, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.LastName, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.LastName, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.IDNumber, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.IDNumber, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.IDNumber, "", New With {.class = "text-danger"})
            </div>

            @*<div class="partelement">
                @Html.LabelFor(Function(model) model., htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.Day, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Day, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.Month, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.Month, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Month, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.Year, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.Year, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Year, "", New With {.class = "text-danger"})
            </div>*@

            <div class="partelement">
                @Html.LabelFor(Function(model) model.RaceNumber, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.RaceNumber, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.RaceNumber, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.EmailAddress, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.EmailAddress, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.EmailAddress, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.MedicalName, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.MedicalName, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.MedicalName, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.MedicalNumber, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.MedicalNumber, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.MedicalNumber, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.EmergencyContact, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.EmergencyContact, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.EmergencyContact, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.EmergencyNumber, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.EmergencyNumber, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.EmergencyNumber, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.BoodType, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.BoodType, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.BoodType, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.Allergies, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.Allergies, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Allergies, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.AdditionalInfo, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.AdditionalInfo, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.AdditionalInfo, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.DoctorName, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.DoctorName, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.DoctorName, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.DoctorContact, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.DoctorContact, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.DoctorContact, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.Clubname, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.Clubname, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Clubname, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.Country, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.Country, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Country, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.Address, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.Address, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Address, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.City, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.City, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.City, "", New With {.class = "text-danger"})
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.Province, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.Province, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Province, "", New With {.class = "text-danger"})
            </div>

            @*<div class="partelement">
                @Html.LabelFor(Function(model) model.UserID, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    @Html.EditorFor(Function(model) model.UserID, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.UserID, "", New With {.class = "text-danger"})
            </div>*@

            <div class="partelement">
                @Html.LabelFor(Function(model) model.EventMailer, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div class="checkbox">
                        @Html.EditorFor(Function(model) model.EventMailer)
                        @Html.ValidationMessageFor(Function(model) model.EventMailer, "", New With {.class = "text-danger"})
                    </div>
            </div>

            <div class="partelement">
                @Html.LabelFor(Function(model) model.Offers, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div class="checkbox">
                        @Html.EditorFor(Function(model) model.Offers)
                        @Html.ValidationMessageFor(Function(model) model.Offers, "", New With {.class = "text-danger"})
                    </div>
            </div>

            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <input type="submit" value="Create" class="btn btn-default" />
                </div>
            </div>
        </div>
    End Using
</div>

