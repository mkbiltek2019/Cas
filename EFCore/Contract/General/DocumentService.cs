﻿using EFCore.DTO;
using EFCore.DTO.General;
using EFCore.Repository;

namespace EFCore.Contract.General
{
	public class DocumentService : Repository<DocumentDTO>, IDocumentService
	{
		public DocumentService()
		{
			var connection = Helper.Helper.GetConnectionString();
			_context = new DataContext(connection);
			_dbset = _context.Set<DocumentDTO>();
		}
	}
}