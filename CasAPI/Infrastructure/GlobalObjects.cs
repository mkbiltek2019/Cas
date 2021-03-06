﻿using System;
using System.Collections.Generic;
using EntityCore.DTO;
using EntityCore.DTO.General;

namespace CasAPI.Infrastructure
{
	public  static class GlobalObjects
	{
		private static Dictionary<Type, List<IBaseDictionary>> _dictionaries;
		private static List<BaseComponentDTO> _baseComponents;

		public static Dictionary<Type, List<IBaseDictionary>> Dictionaries
		{
			get => _dictionaries ?? (_dictionaries = new Dictionary<Type, List<IBaseDictionary>>());
			set => _dictionaries = value;
		}


		public static List<BaseComponentDTO> BaseComponents
		{
			get => _baseComponents ?? (_baseComponents = new List<BaseComponentDTO>());
			set => _baseComponents = value;
		}
	}
}