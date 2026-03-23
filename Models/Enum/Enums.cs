namespace WarehousePro.API.Models.Enums

{



	//public enum UserRole

	//{

	//	Operator,

	//	Supervisor,

	//	Planner,

	//	Logistics,

	//	Admin

	//}




	public enum WarehouseStatus

	{

		Active,

		Inactive

	}

	public enum ZoneType

	{

		Receiving,

		Storage,

		Picking,

		Dispatch

	}

	public enum BinStatus

	{

		Available,

		Full,

		Blocked

	}

	




	public enum ItemStatus

	{

		Active,

		Inactive

	}

	public enum ReservationReferenceType

	{

		Order,

		Replenishment

	}

	



	public enum ReceiptStatus

	{

		Received,

		PartiallyReceived,

		Closed

	}

	public enum PutAwayStatus

	{

		Pending,

		InProgress,

		Completed

	}

	



	public enum OrderStatus

	{

        Created,
		Processing,
        Picking,
        Packed,
        Shipped,
        Delivered,
		Completed,
        
    }

    public enum PickTaskStatus
    {
        
        Created,
        Picked,
        Completed
    }

    public enum PackingStatus

	{

		Packed,

		Shipped

	}

	public enum ShipmentStatus

	{

        Created,
        Dispatched,
        InTransit,
        Delivered

    }

	





	public enum ReplenishmentStatus

	{

		Planned,

		Completed

	}

	public enum SlottingCriterion

	{

		Velocity,

		Weight,

		Category

	}

	public enum SlottingRuleStatus

	{

		Active,

		Inactive

	}

	




	public enum ReportScope

	{

		Warehouse,

		Zone,

		Period

	}

	public enum NotificationCategory

	{

        Inventory,
        Inbound,
        Picking,
        Packing,     
        Shipment,   
        Dispatch

    }

	public enum NotificationStatus

	{

		Unread,

		Read,

		Dismissed

	}

}
