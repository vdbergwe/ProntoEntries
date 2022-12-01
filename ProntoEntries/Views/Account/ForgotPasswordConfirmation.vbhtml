@Code
    ViewBag.Title = "Forgot Password Confirmation"
End Code


<div class="orgcontainer create">
    <div class="titlediv">
        <h2>@ViewBag.Title</h2>
        @Html.ActionLink("Return to Login", "Login")
    </div>
    <hr />

    <div>
        <p style="text-align:center">
            Please check your email to reset your password.
        </p>
    </div>
</div>
    @*<hgroup class="title">
        <h1>@ViewBag.Title.</h1>
    </hgroup>*@
   
