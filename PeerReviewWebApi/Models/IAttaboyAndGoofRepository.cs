
using System.Collections.Generic;

namespace PeerReviewWebApi.Models {
	interface IAttaboyAndGoofRepository {
		Attaboy CreateAttaboy(Attaboy newAttaboy);
		Goof CreateGoof(Goof newGoof);
		IEnumerable<Attaboy> GetAllAttaboys(int userId);
		IEnumerable<Goof> GetAllGoofs(int userId);
		IEnumerable<AttaboyAndGoof> GetAllAttaboysAndGoofs(int userId);
	}
}