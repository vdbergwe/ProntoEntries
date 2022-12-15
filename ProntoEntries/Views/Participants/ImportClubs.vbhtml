@Code
    ViewData("Title") = "ImportClubs"
End Code

<h2>ImportClubs</h2>

@For Each Club In ViewBag.AllClubs
    @<p>
    @Club
</p>
Next