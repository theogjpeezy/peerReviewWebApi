using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace PeerReviewWebApi.Models {
	public class AttaboyAndGoofRepository : IAttaboyAndGoofRepository {

		private const string CREATE_ATTABOY_SPROC = "createAttaboy";
		private const string CREATE_GOOF_SPROC = "createAttaboy";
		private const string GET_ATTABOYS_SPROC = "getAllAttaboys";
		private const string GET_GOOFS_SPROC = "getAllGoofs";
		private const string GET_ALL_ATTABOYS_AND_GOOFS_SPROC = "getAllAttaboysAndGoofs";

		private static readonly DatabaseProviderFactory dbFactory = new DatabaseProviderFactory();
		private readonly Database _peerReviewDb = dbFactory.Create("PeerReviewDatabase");



		public Attaboy CreateAttaboy(Attaboy newAttaboy) {
			int newAttaboyId;
			using (DbCommand createAttaboyCommand = _peerReviewDb.GetStoredProcCommand(CREATE_ATTABOY_SPROC)) {
				createAttaboyCommand.CommandType = CommandType.StoredProcedure;
				_peerReviewDb.AddInParameter(createAttaboyCommand, "userId", DbType.Int16, newAttaboy.UserId);
				_peerReviewDb.AddInParameter(createAttaboyCommand, "submitted", DbType.DateTime, newAttaboy.DateTimeSubmitted);
				_peerReviewDb.AddInParameter(createAttaboyCommand, "comment", DbType.String, newAttaboy.Comment);
				_peerReviewDb.AddInParameter(createAttaboyCommand, "submitterId", DbType.Int16, newAttaboy.SubmitterId);
				_peerReviewDb.AddInParameter(createAttaboyCommand, "isAnonymous", DbType.Boolean, newAttaboy.IsAnonymous);
				
				newAttaboyId = int.Parse(_peerReviewDb.ExecuteScalar(createAttaboyCommand).ToString());
			}

			Attaboy createdAttaboy = newAttaboy;
			createdAttaboy.Id = newAttaboyId;

			return createdAttaboy;
		}

		public Goof CreateGoof(Goof newGoof) {
			int newGoofId;
			using (DbCommand createGoofCommand = _peerReviewDb.GetStoredProcCommand(CREATE_GOOF_SPROC)) {
				createGoofCommand.CommandType = CommandType.StoredProcedure;
				_peerReviewDb.AddInParameter(createGoofCommand, "userId", DbType.Int16, newGoof.UserId);
				_peerReviewDb.AddInParameter(createGoofCommand, "submitted", DbType.DateTime, newGoof.DateTimeSubmitted);
				_peerReviewDb.AddInParameter(createGoofCommand, "comment", DbType.DateTime, newGoof.Comment);
				_peerReviewDb.AddInParameter(createGoofCommand, "submitterId", DbType.String, newGoof.SubmitterId);
				_peerReviewDb.AddInParameter(createGoofCommand, "isAnonymous", DbType.Boolean, newGoof.IsAnonymous);

				newGoofId = (int)_peerReviewDb.ExecuteScalar(createGoofCommand);
			}

			Goof createdGoof = newGoof;
			createdGoof.Id = newGoofId;

			return createdGoof;
			
		}

		public IEnumerable<Attaboy> GetAllAttaboys(int userId) {
			Collection<Attaboy> attaboys = new Collection<Attaboy>();

			using (
				DbCommand getGoalsForUserSproc = _peerReviewDb.GetSqlStringCommand(GET_ATTABOYS_SPROC)) {
				getGoalsForUserSproc.CommandType = CommandType.StoredProcedure;
				_peerReviewDb.AddInParameter(getGoalsForUserSproc, "userId", DbType.Int16, userId);
				using (IDataReader sprocReader = _peerReviewDb.ExecuteReader(getGoalsForUserSproc)) {
					while (sprocReader.Read()) {
						attaboys.Add(new Attaboy {
							Id = (int)sprocReader["feedbackId"],
							DateTimeSubmitted = (DateTime)sprocReader["submitted"],
							Comment = sprocReader["comment"].ToString(),
							IsAnonymous = (bool)sprocReader["isAnonymous"],
							SubmitterId = (int)sprocReader["submitterId"]
						});
					}
				}
			}

			return attaboys;
		}

		public IEnumerable<Goof> GetAllGoofs(int userId) {
			Collection<Goof> goofs = new Collection<Goof>();

			using (
				DbCommand getGoalsForUserSproc = _peerReviewDb.GetSqlStringCommand(GET_GOOFS_SPROC)) {
				getGoalsForUserSproc.CommandType = CommandType.StoredProcedure;
				_peerReviewDb.AddInParameter(getGoalsForUserSproc, "userId", DbType.Int16, userId);
				using (IDataReader sprocReader = _peerReviewDb.ExecuteReader(getGoalsForUserSproc)) {
					while (sprocReader.Read()) {
						goofs.Add(new Goof {
							Id = (int)sprocReader["feedbackId"],
							DateTimeSubmitted = (DateTime)sprocReader["submitted"],
							Comment = sprocReader["comment"].ToString(),
							IsAnonymous = (bool)sprocReader["isAnonymous"],
							SubmitterId = (int)sprocReader["submitterId"]
						});
					}
				}
			}

			return goofs;
		}

		public IEnumerable<AttaboyAndGoof> GetAllAttaboysAndGoofs(int userId) {
            //Collection<AttaboyAndGoof> attaboysAndGoofs = new Collection<AttaboyAndGoof>();
			
            //using (
            //    DbCommand getGoalsForUserSproc = _peerReviewDb.GetSqlStringCommand(GET_ALL_ATTABOYS_AND_GOOFS_SPROC)) {
            //    getGoalsForUserSproc.CommandType = CommandType.StoredProcedure;
            //    _peerReviewDb.AddInParameter(getGoalsForUserSproc, "userId", DbType.Int16, userId);
            //    using (IDataReader sprocReader = _peerReviewDb.ExecuteReader(getGoalsForUserSproc)) {
            //        while (sprocReader.Read()) {
            //            attaboysAndGoofs.Add(new AttaboyAndGoof{
            //                Id = (int)sprocReader["feedbackId"],
            //                DateTimeSubmitted = (DateTime)sprocReader["submitted"],
            //                Comment = sprocReader["comment"].ToString(),
            //                IsAnonymous = (bool)sprocReader["isAnonymous"],
            //                SubmitterId = (int)sprocReader["submitterId"]
            //            });
            //        }
            //    }
            //}
            //return attaboysAndGoofs;
		    throw new NotImplementedException();
		}
	}
}