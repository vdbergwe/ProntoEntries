@ModelType ProntoEntries.RaceEvent
@Code
    ViewData("Title") = "Event Properties"
End Code
<script src="~/Scripts/ckeditor/ckeditor.js"></script>

<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Event Properties</h2>
        @Html.ActionLink("Back to List", "Index")
    </div>
    <hr />

    @Using (Html.BeginForm("Edit", "RaceEvents", FormMethod.Post, New With {.enctype = "multipart/form-data"}))
        @Html.AntiForgeryToken()

        @<div class="form-horizontal1">
    @*<h4>RaceEvent</h4>*@

    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    @Html.HiddenFor(Function(model) model.RaceID)

    <div class="form--el1">


        <div class="form-group">
            @Html.LabelFor(Function(model) model.RaceName, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.RaceName, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.RaceName, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.RaceDescription, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.RaceDescription, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.RaceDescription, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.RaceDate, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.RaceDate, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.RaceDate, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.RaceType, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.RaceType, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.RaceType, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Coordinates, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Coordinates, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.Coordinates, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Address, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Address, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.Address, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.City, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.City, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.City, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Province, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Province, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.Province, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.AdminCharge, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.AdminCharge, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.AdminCharge, "", New With {.class = "text-danger"})
            </div>
        </div>
    </div>

    <div class="form--el2">
        <div class="form-group">
            @Html.LabelFor(Function(model) model.OrgID, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.DropDownList("OrgID", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="")
                @*@Html.EditorFor(Function(model) model.OrgID, New With {.htmlAttributes = New With {.class = "form-control"}})*@
                @Html.ValidationMessageFor(Function(model) model.OrgID, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            <div Class="toggle-groupa">
                <div Class="toggle-itema">
                    @Html.LabelFor(Function(model) model.Image, htmlAttributes:=New With {.class = "toggle-label"})
                    <input type="CheckBox" id="checkboxlogo" onclick="imageloading()" class="switch_1" />
                    <p></p>
                </div>
            </div>
            <div class="col-md-10">
                <div id="newimage">
                    <input type="file" id="imgFile2" name="imgFile2" class="form-control" placeholder=@ViewBag.Image />
                </div>
                <div id="currentimage">
                    @Html.EditorFor(Function(model) model.Image, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>
                @Html.ValidationMessageFor(Function(model) model.Image, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            <div Class="toggle-groupa">
                <div Class="toggle-itema">
                    @Html.LabelFor(Function(model) model.Background, htmlAttributes:=New With {.class = "toggle-label"})
                    <input type="CheckBox" id="checkboxbackground" onclick="Backgroundloading()" class="switch_1" />
                    <p></p>
                </div>
            </div>
            <div class="col-md-10">
                <div id="newimageback">
                    <input type="file" id="imgBackground" name="imgBackground" class="form-control" placeholder=@ViewBag.Background />
                </div>
                <div id="currentimageback">
                    @Html.EditorFor(Function(model) model.Background, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>
                @Html.ValidationMessageFor(Function(model) model.Background, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div Class="form-group">
            <div Class="toggle-groupa">
                <div Class="toggle-itema">
                    @Html.LabelFor(Function(model) model.ImgDetail1, htmlAttributes:=New With {.class = "toggle-label"})
                    <input type="CheckBox" id="checkboxfirst" onclick="firstimageloading()" class="switch_1" />
                    <p></p>
                </div>
            </div>
            <div Class="col-md-10">
                <div id="newimagefirst">
                    <input type="file" id="imgdetailfirst" name="imgdetailfirst" class="form-control" placeholder=@ViewBag.Background />
                </div>
                <div id="currentimagefirst">
                    @Html.EditorFor(Function(model) model.ImgDetail1, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>
                @Html.ValidationMessageFor(Function(model) model.ImgDetail1, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div Class="form-group">
            <div Class="toggle-groupa">
                <div Class="toggle-itema">
                    @Html.LabelFor(Function(model) model.ImgDetail2, htmlAttributes:=New With {.class = "toggle-label"})
                    <input type="CheckBox" id="checkboxsecond" onclick="secondimageloading()" class="switch_1" />
                    <p></p>
                </div>
            </div>
            <div Class="col-md-10">
                <div id="newimagesecond">
                    <input type="file" id="imgdetailsecond" name="imgdetailsecond" class="form-control" placeholder=@ViewBag.Background />
                </div>
                <div id="currentimagesecond">
                    @Html.EditorFor(Function(model) model.ImgDetail2, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>
                @Html.ValidationMessageFor(Function(model) model.ImgDetail2, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div Class="toggle-groupa">
            <div Class="toggle-itema">
                @Html.LabelFor(Function(model) model.ImgDetail3, htmlAttributes:=New With {.class = "toggle-label"})
                <input type="CheckBox" id="checkboxthird" onclick="thirdimageloading()" class="switch_1" placeholder="Replace" />
                <p></p>
            </div>
        </div>
        <div Class="form-group">
            <div Class="col-md-10">
                <div id="newimagethird">
                    <input type="file" id="imgdetailthird" name="imgdetailthird" class="form-control" placeholder=@ViewBag.Background />
                </div>
                <div id="currentimagethird">
                    @Html.EditorFor(Function(model) model.ImgDetail3, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>
                @Html.ValidationMessageFor(Function(model) model.ImgDetail3, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div Class="toggle-group">
            <div Class="toggle-item">
                @Html.LabelFor(Function(model) model.DispClasses, htmlAttributes:=New With {.class = "toggle-label"})
                @Html.CheckBoxFor(Function(model) model.DispClasses, New With {.htmlAttributes = New With {.class = "switch_1"}})
                @Html.ValidationMessageFor(Function(model) model.DispClasses, "", New With {.class = "text-danger"})
            </div>
            <div Class="toggle-item">
                @Html.LabelFor(Function(model) model.DispAdmCharge, htmlAttributes:=New With {.class = "toggle-label"})
                @Html.CheckBoxFor(Function(model) model.DispAdmCharge, New With {.htmlAttributes = New With {.class = "toggle-checkbox"}})
                @Html.ValidationMessageFor(Function(model) model.DispAdmCharge, "", New With {.class = "text-danger"})
            </div>
            <div Class="toggle-item">
                @Html.LabelFor(Function(model) model.DispAddress, htmlAttributes:=New With {.class = "toggle-label", .id = "labelDispAddress"})
                @Html.CheckBoxFor(Function(model) model.DispAddress, New With {.htmlAttributes = New With {.class = "toggle-checkbox"}})
                @Html.ValidationMessageFor(Function(model) model.DispAddress, "", New With {.class = "text-danger"})
            </div>
            <div Class="toggle-item">
                @Html.LabelFor(Function(model) model.PF_ToClient, htmlAttributes:=New With {.class = "toggle-label", .id = "labelDispAddress"})
                @Html.CheckBoxFor(Function(model) model.PF_ToClient, New With {.htmlAttributes = New With {.class = "toggle-checkbox"}})
                @Html.ValidationMessageFor(Function(model) model.PF_ToClient, "", New With {.class = "text-danger"})
            </div>
            <div Class="toggle-item">
                @Html.LabelFor(Function(model) model.Admin_ToClient, htmlAttributes:=New With {.class = "toggle-label", .id = "labelDispAddress"})
                @Html.CheckBoxFor(Function(model) model.Admin_ToClient, New With {.htmlAttributes = New With {.class = "toggle-checkbox"}})
                @Html.ValidationMessageFor(Function(model) model.Admin_ToClient, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Admin_Rate, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Admin_Rate, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.Admin_Rate, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Indemnity, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Indemnity, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.Indemnity, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.TandC, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.TandC, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.TandC, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.EntriesCloseDate, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.EntriesCloseDate, New With {.htmlAttributes = New With {.class = "form-control"}})

                @Html.ValidationMessageFor(Function(model) model.EntriesCloseDate, "", New With {.class = "text-danger"})
            </div>
        </div>
    </div>

    <div class="form--el1">

        <div class="form-group">
            @Html.LabelFor(Function(model) model.RaceHtmlPage, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.TextAreaFor(Function(model) model.RaceHtmlPage, New With {.htmlAttributes = New With {.class = "form-control"}, .id = "FullDescription"})
                @Html.ValidationMessageFor(Function(model) model.RaceHtmlPage, "", New With {.class = "text-danger"})
            </div>
        </div>

        <script>
            CKEDITOR.replace("FullDescription");
        </script>
    </div>

        <div Class="button-group">
            <input type="submit" value="Update" Class="btn btn-default" />
        </div>
    </div>
    End Using
</div>



@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
    <script>
        $(document).ready(function () {
            document.getElementById("newimage").style.display = "none";
            document.getElementById("currentimage").display = "block";

            document.getElementById("newimageback").style.display = "none";
            document.getElementById("currentimageback").style.display = "block";

            document.getElementById("newimagefirst").style.display = "none";
            document.getElementById("currentimagefirst").style.display = "block";

            document.getElementById("newimagesecond").style.display = "none";
            document.getElementById("currentimagesecond").style.display = "block";

            document.getElementById("newimagethird").style.display = "none";
            document.getElementById("currentimagethird").style.display = "block";
        })

        function imageloading() {
            if (document.getElementById("checkboxlogo").checked) {
                document.getElementById("newimage").style.display = "block";
                document.getElementById("currentimage").style.display = "none";
            } else {
                document.getElementById("newimage").style.display = "none";
                document.getElementById("currentimage").style.display = "block";
            }
        }

        function Backgroundloading() {
            if (document.getElementById("checkboxbackground").checked) {
                document.getElementById("newimageback").style.display = "block";
                document.getElementById("currentimageback").style.display = "none";
            } else {
                document.getElementById("newimageback").style.display = "none";
                document.getElementById("currentimageback").style.display = "block";
            }
        }

        function firstimageloading() {
            if (document.getElementById("checkboxfirst").checked) {
                document.getElementById("newimagefirst").style.display = "block";
                document.getElementById("currentimagefirst").style.display = "none";
            } else {
                document.getElementById("newimagefirst").style.display = "none";
                document.getElementById("currentimagefirst").style.display = "block";
            }
        }

        function secondimageloading() {
            if (document.getElementById("checkboxsecond").checked) {
                document.getElementById("newimagesecond").style.display = "block";
                document.getElementById("currentimagesecond").style.display = "none";
            } else {
                document.getElementById("newimagesecond").style.display = "none";
                document.getElementById("currentimagesecond").style.display = "block";
            }
        }

        function thirdimageloading() {
            if (document.getElementById("checkboxthird").checked) {
                document.getElementById("newimagethird").style.display = "block";
                document.getElementById("currentimagethird").style.display = "none";
            } else {
                document.getElementById("newimagethird").style.display = "none";
                document.getElementById("currentimagethird").style.display = "block";
            }
        }
    </script>
End Section
