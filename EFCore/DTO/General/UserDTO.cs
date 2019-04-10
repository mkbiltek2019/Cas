﻿using System.Runtime.Serialization;
using System.Security.Principal;
using EFCore.Attributte;
using EFCore.Interfaces;

namespace EFCore.DTO.General
{
	public enum UsetType
	{
		Admin,
		User
	}

	[DataContract(IsReference = true)]
	[Condition("IsDeleted", 0)]
	public class UserDTO : BaseEntity, IIdentityUser
	{
		private static UserDTO Unknown1;

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Surname { get; set; }

		[DataMember]
		public string Login { get; set; }

		[DataMember]
		public string Password { get; set; }

		[DataMember]
		public UsetType UserType { get; set; }

		public override string ToString()
		{
			return Name.Equals(Surname) ? Name : $"{Surname} {Name}";
		}

		public static UserDTO Unknown => Unknown1 ?? (Unknown1 = new UserDTO{ Name = "Unknown", ItemId = -1});
	}

	public interface IIdentityUser : IBaseEntity
	{
		string Name { get; set; }
		string Surname { get; set; }
		string Login { get; set; }
		string Password { get; set; }
		UsetType UserType { get; set; }

	}
}