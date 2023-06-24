using API.Contracts;
using API.Repositories;
using API.ViewModel.Account;
using API.ViewModel.Other;
using API.ViewModel.Task;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class TaskController : BaseController<Model.Task, TaskVM>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper<Model.Task, TaskVM> _mapper;

        public TaskController(ITaskRepository taskRepository, IMapper<Model.Task, TaskVM> mapper) : base(taskRepository,mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
    }
}
