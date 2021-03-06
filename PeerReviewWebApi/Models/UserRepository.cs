﻿using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace PeerReviewWebApi.Models {
	public class UserRepository : IUserRepository {

		private User _user = new User();
		private const string GET_USER_SPROC = "getUserInfo";
		private const string GET_MANAGER_SPROC = "getManagerOfUser";
		private const string GET_GOALS_SPROC = "getAllActiveGoalsForUser";
		private const string GET_TEAM_SPROC = "getTeammatesOfUser";

		public User Get(int id) {
			
			_user = new User();
			
			// connect to the DB
			DatabaseProviderFactory dbFactory = new DatabaseProviderFactory();
		    Database peerReviewDb = dbFactory.Create("PeerReviewDatabase");
			
			// Get the basic user information
			using (DbCommand getUserCommand = peerReviewDb.GetSqlStringCommand(GET_USER_SPROC)) {
				getUserCommand.CommandType = CommandType.StoredProcedure;
				peerReviewDb.AddInParameter(getUserCommand, "userId", DbType.Int16, id);

				using (IDataReader sprocReader = peerReviewDb.ExecuteReader(getUserCommand)) {
					while (sprocReader.Read()) {
						_user.Id = id;
						_user.FirstName = sprocReader["firstName"].ToString();
						_user.LastName = sprocReader["lastName"].ToString();
						_user.Email = sprocReader["email"].ToString();
					}
				}
			}

			// Get the users's goals
			GoalRepository goalRepo = new GoalRepository();
			_user.Goals = goalRepo.GetGoalsByUserId(id);

			// Get the user's manager
			using (DbCommand getUserManagerSproc = peerReviewDb.GetSqlStringCommand(GET_MANAGER_SPROC)) {
				getUserManagerSproc.CommandType = CommandType.StoredProcedure;
				peerReviewDb.AddInParameter(getUserManagerSproc, "userId", DbType.Int16, id);

				using (IDataReader sprocReader = peerReviewDb.ExecuteReader(getUserManagerSproc)) {
					while (sprocReader.Read()) {
						// more than one manager could be bad?  worry about this later
						_user.ManagerName = sprocReader["firstName"] + " " + sprocReader["lastName"];
					}
				}
			}

			// Get the user's team
			Collection<User> teamMembers = new Collection<User>();

			using (DbCommand getUserTeamSproc = peerReviewDb.GetSqlStringCommand(GET_TEAM_SPROC)) {
				getUserTeamSproc.CommandType = CommandType.StoredProcedure;
				peerReviewDb.AddInParameter(getUserTeamSproc,"userId",DbType.Int16,id);

				using (IDataReader sprocReader = peerReviewDb.ExecuteReader(getUserTeamSproc)) {
					while (sprocReader.Read()) {
						teamMembers.Add(new User {
						    FirstName = sprocReader["firstName"].ToString(),
                            LastName = sprocReader["lastName"].ToString(),
                            Id = int.Parse(sprocReader["id"].ToString())
						});			
					}
				}
				_user.TeamMembers = teamMembers;
			}
			
			// Send back the result
			return _user;
		}
	}
}