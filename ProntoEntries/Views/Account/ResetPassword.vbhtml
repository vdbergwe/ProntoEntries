@ModelType ResetPasswordViewModel
@Code
    ViewBag.Title = "Reset password"
End Code

<div class="orgcontainer create">
    <div class="titlediv">
        <h2>@ViewBag.Title</h2>
    </div>
    <hr />


    @Using Html.BeginForm("ResetPassword", "Account", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})

        @Html.AntiForgeryToken()

        @<text>

            @Html.ValidationSummary("", New With {.class = "text-danger"})
            @Html.HiddenFor(Function(m) m.Code)
            <div class="form-group">
                @*@Html.LabelFor(Function(m) m.Email, New With {.class = "col-md-2 control-label"})*@
                <div class="col-md-10">
                    @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(m) m.Email)})
                </div>
            </div>
            <div class="form-group">
                @*@Html.LabelFor(Function(m) m.Password, New With {.class = "col-md-2 control-label"})*@
                <div class="col-md-10">
                    @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(m) m.Password)})
                </div>
            </div>
            <div class="form-group">
                @*@Html.LabelFor(Function(m) m.ConfirmPassword, New With {.class = "col-md-2 control-label"})*@
                <div class="col-md-10">
                    @Html.PasswordFor(Function(m) m.ConfirmPassword, New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(m) m.ConfirmPassword)})
                </div>
            </div>

            <div class="button-group">
                <input type="submit" class="btn btn-default" value="Reset" />
            </div>

        </text>
    End Using
</div>

    @section Scripts
        @Scripts.Render("~/bundles/jqueryval")
    End Section
