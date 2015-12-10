using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace PeerReviewWebApi.Models {
	public class UserRepository : IUserRepository {

		private User _user = new User();
		private const string GET_USER_SPROC = "getUserInfo";

		public User Get(int id) {
			
			_user = new User();
			
			// connect to the DB
			DatabaseProviderFactory dbFactory = new DatabaseProviderFactory();
		    Database peerReviewDB = dbFactory.Create("PeerReviewDatabase");
			
			// Get the basic user information
			using (DbCommand dbCommand = peerReviewDB.GetSqlStringCommand(GET_USER_SPROC)) {
				dbCommand.CommandType = CommandType.StoredProcedure;
				peerReviewDB.AddInParameter(dbCommand, "userId", DbType.Int16, id);

				using (IDataReader sprocReader = peerReviewDB.ExecuteReader(dbCommand)) {
					while (sprocReader.Read()) {
						_user.Id = id;
						_user.FirstName = sprocReader["firstName"].ToString();
						_user.LastName = sprocReader["lastName"].ToString();
						_user.Email = sprocReader["email"].ToString();
					}
				}
			}

			// Get the user's manager

			// Get the user's team

			// Get the user's goals
			
			// Send back the result
			return _user;
		}
	}
}