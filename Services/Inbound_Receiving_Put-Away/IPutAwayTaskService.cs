
using WarehouseProject.DTOs.PutAwayTaskDTOs;

public interface IPutAwayTaskService

{

    // ✅ GET ALL

    Task<IEnumerable<PutAwayTaskResponseDTO>> GetAll();

    // ✅ GET BY ID

    Task<PutAwayTaskResponseDTO> GetById(int id);

    // 🔥 MAIN PUTAWAY (CREATE + UPDATE + INVENTORY)

    Task<string> PutAwayAsync(CreatePutAwayTaskDTO dto);

    // ✅ UPDATE

    Task<PutAwayTaskResponseDTO> Update(int id, UpdatePutAwayTaskDTO dto);

    // ✅ DELETE

    Task<bool> Delete(int id);

}
