using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class AbstractControllerBase : ControllerBase { }
