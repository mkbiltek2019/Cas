﻿using System;
using EntityCore.DTO;
using EntityCore.DTO.Dictionaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CasAPI.Controllers.Dictionaries
{
	[Route("accessorydescription")]
	public class AccessoryDescriptionController : BaseController<AccessoryDescriptionDTO>
	{
		private Type _type = typeof(AccessoryDescriptionDTO);


		public AccessoryDescriptionController(DataContext context, ILogger<BaseController<AccessoryDescriptionDTO>> logger) : base(context, logger)
		{
			
		}
	}
}