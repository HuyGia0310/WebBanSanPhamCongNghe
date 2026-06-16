# Agents Rules

## Mục tiêu
Tài liệu này ghi các quy tắc làm việc để giữ codebase ổn định, dễ bảo trì và tránh thay đổi không cần thiết.

## Quy tắc bắt buộc

1. Ưu tiên tái sử dụng code cũ trước khi viết mới.
2. Chỉ thêm code mới khi không thể tái sử dụng hợp lý từ phần hiện có.
3. Hạn chế xóa code, file, hoặc logic đang hoạt động nếu chưa thật sự cần thiết.
4. Khi cần sửa, ưu tiên chỉnh sửa nhỏ, có mục tiêu rõ ràng, tránh refactor rộng nếu không được yêu cầu.
5. Không tự ý thay đổi cấu trúc dữ liệu hoặc luồng nghiệp vụ lớn nếu chưa đánh giá tác động.
6. Nếu cần thêm, sửa, hoặc xóa liên quan đến cơ sở dữ liệu thì phải đề xuất trước.
7. Với thay đổi CSDL, cần nêu rõ:
   - bảng / cột / ràng buộc bị ảnh hưởng
   - lý do thay đổi
   - rủi ro có thể phát sinh
   - cách kiểm tra sau khi áp dụng
8. Nếu có thể giải quyết bằng cách dùng lại hàm, model, view, controller, helper, hoặc component sẵn có thì phải ưu tiên cách đó.
9. Trước khi xóa bất kỳ phần nào, phải kiểm tra xem nó có đang được dùng ở nơi khác không.
10. Nếu chưa chắc chắn, hãy dừng lại và đề xuất phương án thay vì sửa trực tiếp.

## Nguyên tắc làm việc

- Giữ thay đổi nhỏ và rõ ràng.
- Tôn trọng cấu trúc hiện tại của dự án.
- Không phá vỡ chức năng đang chạy chỉ để “làm gọn” code.
- Ưu tiên an toàn hơn là tối ưu hóa quá sớm.

