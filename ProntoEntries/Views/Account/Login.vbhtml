@ModelType LoginViewModel
@Code
    ViewBag.Title = "Log in"
End Code

<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Log In</h2>
        @Html.ActionLink("Register New User", "Register")
    </div>

    <hr />

    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                @Using Html.BeginForm("Login", "Account", New With {.ReturnUrl = ViewBag.ReturnUrl}, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
                    @Html.AntiForgeryToken()
                    @<text>
                        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                        <div class="form-group">
                            @*@Html.LabelFor(Function(m) m.Email, New With {.class = "col-md-2 control-label"})*@
                            <div class="col-md-10">
                                @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Email)})
                                @Html.ValidationMessageFor(Function(m) m.Email, "", New With {.class = "text-danger"})
                            </div>
                        </div>
                        <div class="form-group">
                            @*@Html.LabelFor(Function(m) m.Password, New With {.class = "col-md-2 control-label"})*@
                            <div class="col-md-10">
                                @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Password)})
                                @Html.ValidationMessageFor(Function(m) m.Password, "", New With {.class = "text-danger"})
                            </div>
                        </div>
                        <div class="button-group">
                            <div class="col-md-10">
                                <div class="checkbox">
                                    @Html.CheckBoxFor(Function(m) m.RememberMe, New With {.class = "toggle-checkbox"})
                                    Remember Me
                                </div>
                            </div>
                        </div>

                        <div class="button-group">                            
                                <input type="submit" value="Log in" class="btn btn-default" />                          
                        </div>

                        @* Enable this once you have account confirmation enabled for password reset functionality
                <p>
                    @Html.ActionLink("Forgot your password?", "ForgotPassword")
                </p>*@
                    </text>
                End Using
            </section>
        </div>
        @*<div class="col-md-4">
            <section id="socialLoginForm">
                @Html.Partial("_ExternalLoginsListPartial", New ExternalLoginListViewModel With {.ReturnUrl = ViewBag.ReturnUrl})
            </section>
        </div>*@
    </div>
</div>
        @Section Scripts
            @Scripts.Render("~/bundles/jqueryval")
        End Section
