﻿@ModelType IEnumerable(Of ProntoEntries.Participant)
@Code
    ViewData("Title") = "User Profiles"
End Code

<div class="orgcontainer">
    <div class="titlediv">
        <h2>User Profiles</h2>
        @Html.ActionLink("Create New", "Create")
    </div>

    @If User.Identity.IsAuthenticated And ((User.IsInRole("Admin"))) Then
        @<form action="" method="get">
            <div Class="DropdownSearches">
                <div Class="DropdownSearches item">
                    <p> Search Value</p>
                    <input type="text" name="SearchValue" id="SearchValue" Class="form-control" placeholder="@ViewBag.SearchText" />
                </div>
            </div>
        </form>
    End If

    <hr />
    <table class="table">
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.FirstName)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.LastName)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.IDNumber)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.RaceNumber)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.EmailAddress)
            </th>
        </tr>

        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.FirstName)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.LastName)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.IDNumber)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.RaceNumber)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.EmailAddress)
                </td>
                <td>
                    @Html.ActionLink("Edit", "Edit", New With {.id = item.ParticipantID}) @*|

        @Html.ActionLink("Details", "Details", New With {.id = item.ParticipantID}) *@ | 
                    @Html.Action("Get_IsInRace", New With {.Id = item.ParticipantID})
                </td>
            </tr>
        Next

    </table>
</div>
