using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CodingSchool.TodoAPI.Models;

namespace CodingSchool.TodoAPI.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        #region Todo List

        private readonly static List<Todo> TodoList = new List<Todo>();

        static TodoController()
        {
            TodoList.Add(new Todo { Id = 1, Name = "Catch all 648 Pokemons", Completed = false });
            TodoList.Add(new Todo { Id = 2, Name = "Destroy the one ring in Mordor", Completed = false });
            TodoList.Add(new Todo { Id = 3, Name = "Save Princess Peach", Completed = true });
            TodoList.Add(new Todo { Id = 4, Name = "Go back to 1985", Completed = true });
        }

        #endregion

        // GET api/todo
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(TodoList);
        }

        [HttpGet]
        [Route("/api/something/random")]
        public ActionResult GetRandom()
        {
            if (TodoList.Count == 0)
            {
                return NotFound();
            }

            var index = new Random().Next(0, TodoList.Count);
            var todo = TodoList[index];

            return Ok(todo);
        }

        // GET api/todo/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var todo = TodoList.FirstOrDefault(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // POST api/todo
        [HttpPost]
        public ActionResult Post([FromBody] TodoCreate model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name))
            {
                return BadRequest();
            }

            var todo = new Todo
            {
                Id = TodoList.Count + 1,
                Name = model.Name,
                Completed = false,
            };

            TodoList.Add(todo);

            return CreatedAtAction("Get", todo);
        }

        // PUT api/todo/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TodoUpdate model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name))
            {
                return BadRequest();
            }

            var todo = TodoList.FirstOrDefault(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            todo.Name = model.Name;
            todo.Completed = model.Completed;

            return Ok(todo);
        }

        // DELETE api/todo/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var todo = TodoList.FirstOrDefault(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            TodoList.Remove(todo);

            return NoContent();
        }
    }
}