﻿@ModelType ProntoEntries.Participant
@Code
    ViewData("Title") = "New User Profile"
End Code

<div class="orgcontainer partcreate">
    <div class="titlediv">
        <h2>New User Profile</h2>
        @Html.ActionLink("Back to List", "Index")
    </div>
    <hr />


    @Using (Html.BeginForm(New With {.EventId = ViewBag.EventID, .Distance = ViewBag.Distance}))
        @Html.AntiForgeryToken()
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

        @<div class="participantcontainer">

            <div class="participantleft">
                <p>Personal Information</p>
            </div>

            <div class="participantright">
                <div class="partelement">
                    @Html.LabelFor(Function(model) model.FirstName, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.FirstName, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.FirstName, "", New With {.class = "text-danger"})
                </div>

                <div class="partelement">
                    @Html.LabelFor(Function(model) model.MiddleNames, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.MiddleNames, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.MiddleNames, "", New With {.class = "text-danger"})
                </div>

                <div class="partelement">
                    @Html.LabelFor(Function(model) model.LastName, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.LastName, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.LastName, "", New With {.class = "text-danger"})
                </div>

                <div Class="partelement">
                    @Html.LabelFor(Function(model) model.EmailAddress, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.EmailAddress, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.EmailAddress, "", New With {.class = "text-danger"})
                </div>

                <div Class="partelement">
                    @Html.LabelFor(Function(model) model.Mobile, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.Mobile, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.Mobile, "", New With {.class = "text-danger"})
                </div>

                <div class="partelement">
                    @Html.LabelFor(Function(model) model.IDNumber, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.IDNumber, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @If ViewBag.InvalidID = True Then
                        @Html.ValidationMessageFor(Function(model) model.IDNumber, "ID MUST BE UNIQUE", New With {.class = "text-danger"})
                    Else
                        @Html.ValidationMessageFor(Function(model) model.IDNumber, "", New With {.class = "text-danger"})
                    End If
                </div>

                <div class="partelement">
                    @Html.LabelFor(Function(model) model.Gender, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.DropDownList("Gender", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="")
                    @*@Html.EditorFor(Function(model) model.Gender, New With {.htmlAttributes = New With {.class = "partelement item"}})*@
                    @Html.ValidationMessageFor(Function(model) model.Gender, "", New With {.class = "text-danger"})
                </div>

                <div class="partelement">
                    @Html.LabelFor(Function(model) model.DOB, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.DOB, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.DOB, "", New With {.class = "text-danger"})
                </div>
            </div>
        </div>
        @<hr />
        @<div class="participantcontainer">

            <div Class="participantleft">
                <p>Medical Information</p>
            </div>

            <div Class="participantright">

                <div Class="partelement">
                    @Html.LabelFor(Function(model) model.MedicalName, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.MedicalName, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.MedicalName, "", New With {.class = "text-danger"})
                </div>

                <div Class="partelement">
                    @Html.LabelFor(Function(model) model.MedicalNumber, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.MedicalNumber, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.MedicalNumber, "", New With {.class = "text-danger"})
                </div>

                <div Class="partelement">
                    @Html.LabelFor(Function(model) model.EmergencyContact, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.EmergencyContact, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.EmergencyContact, "", New With {.class = "text-danger"})
                </div>

                <div Class="partelement">
                    @Html.LabelFor(Function(model) model.EmergencyNumber, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.EmergencyNumber, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.EmergencyNumber, "", New With {.class = "text-danger"})
                </div>

                <div Class="partelement">
                    @Html.LabelFor(Function(model) model.BoodType, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.DropDownList("BoodType", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="")
                    @*@Html.EditorFor(Function(model) model.BoodType, New With {.htmlAttributes = New With {.class = "partelement item"}})*@
                    @Html.ValidationMessageFor(Function(model) model.BoodType, "", New With {.class = "text-danger"})
                </div>

                <div Class="partelement">
                    @Html.LabelFor(Function(model) model.Allergies, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.Allergies, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.Allergies, "", New With {.class = "text-danger"})
                </div>

                <div Class="partelement">
                    @Html.LabelFor(Function(model) model.AdditionalInfo, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.AdditionalInfo, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.AdditionalInfo, "", New With {.class = "text-danger"})
                </div>

                <div Class="partelement">
                    @Html.LabelFor(Function(model) model.DoctorName, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.DoctorName, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.DoctorName, "", New With {.class = "text-danger"})
                </div>

                <div Class="partelement">
                    @Html.LabelFor(Function(model) model.DoctorContact, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.DoctorContact, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.DoctorContact, "", New With {.class = "text-danger"})
                </div>
            </div>
        </div>

        @<hr />
        @<div class="participantcontainer">

            <div Class="participantleft">
                <p>Club Information</p>
            </div>

            <div Class="participantright">
                <div class="partelement">
                    @Html.LabelFor(Function(model) model.Clubname, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})

                    @*@Html.DropDownList("Clubname", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="")*@
                    @Html.EditorFor(Function(model) model.Clubname, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.Clubname, "", New With {.class = "text-danger"})
                </div>

                <div class="partelement">
                    @Html.LabelFor(Function(model) model.RaceNumber, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.RaceNumber, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.RaceNumber, "", New With {.class = "text-danger"})
                </div>
            </div>
        </div>

        @<hr />
        @<div class="participantcontainer">

            <div Class="participantleft">
                <p>Address</p>
            </div>

            <div Class="participantright">
                <div class="partelement">
                    @Html.LabelFor(Function(model) model.Country, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.Country, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.Country, "", New With {.class = "text-danger"})
                </div>

                <div class="partelement">
                    @Html.LabelFor(Function(model) model.Address, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.Address, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.Address, "", New With {.class = "text-danger"})
                </div>

                <div class="partelement">
                    @Html.LabelFor(Function(model) model.City, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.EditorFor(Function(model) model.City, New With {.htmlAttributes = New With {.class = "partelement item"}})
                    @Html.ValidationMessageFor(Function(model) model.City, "", New With {.class = "text-danger"})
                </div>

                <div class="partelement">
                    @Html.LabelFor(Function(model) model.Province, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    @Html.DropDownList("Province", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="")
                    @*@Html.EditorFor(Function(model) model.Province, New With {.htmlAttributes = New With {.class = "partelement item"}})*@
                    @Html.ValidationMessageFor(Function(model) model.Province, "", New With {.class = "text-danger"})
                </div>
            </div>
        </div>

        @<hr />
        @<div class="participantcontainer">

            <div Class="participantleft">
                <p>Marketing Concent</p>
            </div>

            <div Class="participantright">
                <div class="partelement">
                    @Html.LabelFor(Function(model) model.EventMailer, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    <div class="checkbox">
                        @Html.CheckBoxFor(Function(model) model.EventMailer, New With {.class = "toggle-checkbox"})
                        @*@Html.EditorFor(Function(model) model.EventMailer)*@
                        @Html.ValidationMessageFor(Function(model) model.EventMailer, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="partelement">
                    @Html.LabelFor(Function(model) model.Offers, htmlAttributes:=New With {.class = "control-label col-md-2 labelfix"})
                    <div class="checkbox fix">
                        @Html.EditorFor(Function(model) model.Offers)
                        @Html.ValidationMessageFor(Function(model) model.Offers, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    <p></p>
                    <input type="submit" value="Create" class="btn btn-default" />
                </div>
            </div>
        </div>
    End Using
</div>


@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
    @*<script type="text/javascript">
        $(function () {
            $("#Clubname").chosen();
        });
    </script>*@
End Section

