using System.Diagnostics;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shop_Express_Test_Task.Models;

namespace Shop_Express_Test_Task.Controllers;

public class TasksController : Controller
{
	private const string kInvalidParamsMessage = "Invalid params";

	private readonly ITaskRepository _tasksRepository;

	public TasksController(ITaskRepository tasksRepository)
	{
		_tasksRepository = tasksRepository;
	}

	public async Task<IActionResult> Index()
	{
		var tasks = await _tasksRepository.GetAll();
		tasks = tasks.OrderByDescending(task => task.CreatedDate).ToList();
		return View(tasks);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}

	[HttpPost]
	public async Task<IActionResult> Add(string title)
	{
		if (IsTitleValid(title) == false)
			return BadRequest(kInvalidParamsMessage);

		var newTask = new ToDoTask() { Title = title, CreatedDate = DateTime.Now };
		await _tasksRepository.Add(newTask);
		return Ok(new JsonResult(newTask));
	}

	[HttpDelete]
	public IActionResult Delete(int id)
	{
		if (IsIdValid(id) == false)
			return BadRequest();

		_tasksRepository.DeleteById(id);
		return Ok(new { Id = id });
	}

	[HttpPatch]
	public async Task<IActionResult> MarkCompleted(int id)
	{
		if (IsIdValid(id) == false)
			return BadRequest(kInvalidParamsMessage);

		var task = await _tasksRepository.GetById(id);
		task.IsCompleted = true;
		await _tasksRepository.Update(task);
		return Ok();
	}

	[HttpPatch]
	public async Task<IActionResult> Update(int id, string title)
	{
		if (IsIdValid(id) == false)
			return BadRequest(kInvalidParamsMessage);
		if (IsTitleValid(title) == false)
			return BadRequest(kInvalidParamsMessage);

		var task = await _tasksRepository.GetById(id);
		task.Title = title;
		await _tasksRepository.Update(task);
		return Ok(task);
	}

	private bool IsIdValid(int id) => id > 0;

	private bool IsTitleValid(string title)
	{
		if (string.IsNullOrEmpty(title) || title.Length < 3)
			return false;
		return true;
	}
}
