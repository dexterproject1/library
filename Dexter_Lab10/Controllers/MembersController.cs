using Npgsql;
using library.Data;
using library.Models;
using Microsoft.AspNetCore.Mvc;

namespace library.Controllers;

public class MembersController : Controller
{
    private readonly MemberRepository _members;

    public MembersController(MemberRepository members)
    {
        _members = members;
    }

    // GET /Members : the list of members.
    public async Task<IActionResult> Index()
    {
        var members = await _members.GetAllAsync();
        return View(members);
    }

    // GET /Members/Create : an empty form.
    public IActionResult Create()
    {
        return View(new Member());
    }

    // POST /Members/Create : the form was submitted.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Member member)
    {
        if (!ModelState.IsValid)
        {
            return View(member);
        }

        await _members.AddAsync(member);
        return RedirectToAction(nameof(Index));
    }

    // GET /Members/Edit/5 : the form, filled with member 5.
    public async Task<IActionResult> Edit(long id)
    {
        var member = await _members.GetByIdAsync(id);
        if (member == null)
        {
            return NotFound();
        }
        return View(member);
    }

    // POST /Members/Edit/5 : the changed form was submitted.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Member member)
    {
        if (!ModelState.IsValid)
        {
            return View(member);
        }

        await _members.UpdateAsync(member);
        return RedirectToAction(nameof(Index));
    }

    // GET /Members/Delete/5 : ask "are you sure?"
    public async Task<IActionResult> Delete(long id)
    {
        var member = await _members.GetByIdAsync(id);
        if (member == null)
        {
            return NotFound();
        }
        return View(member);
    }

    // POST /Members/Edit/5 : the changed form was submitted.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Member member)
    {
        if (/* 1. a rule on the form was broken */)
        {
            // 2. Show the form again, with what was typed.
        }

        try
        {
            // 3. Save the changes.
        }
        catch (PostgresException ex) when (/* 4. another member already uses this email */)
        {
            // 5. Put a message under the Email box.
            // 6. Show the form again, with what was typed.
        }

        // 7. The changes were saved: go back to the list.
    }
}