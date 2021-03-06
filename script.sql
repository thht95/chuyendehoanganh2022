alter table Tieuchi
alter column Noidung nvarchar(100)

go
create proc allSinhvien
as
begin
	select Sinhvien.ID, Sinhvien.Ten, NgayDanhgia, SUM(Diemdanhgia) as 'Điểm đánh giá' from Sinhvien, Lop, Danhgia
	where Sinhvien.ID = Danhgia.SinhvienID
	and Sinhvien.LopID = Lop.ID
	group by Sinhvien.ID,  Sinhvien.Ten, Ngaydanhgia
	order by Ngaydanhgia, Ten
end

go
create proc allSinhvienByNgayDanhgia
@Ngaydanhgia date
as
begin
	select Sinhvien.ID, Sinhvien.Ten, NgayDanhgia, SUM(Diemdanhgia) as 'Điểm đánh giá' from Sinhvien, Lop, Danhgia
	where Sinhvien.ID = Danhgia.SinhvienID
	and Sinhvien.LopID = Lop.ID
	and Ngaydanhgia = @Ngaydanhgia
	group by Sinhvien.ID,  Sinhvien.Ten, Ngaydanhgia
	order by Ngaydanhgia, Ten
end

ALTER proc [dbo].[allSinhvienByNgayDanhgia]
@Ngaydanhgia date
as
begin
	select Sinhvien.ID, Sinhvien.Ten, Lop.Ten, NgayDanhgia, SUM(Diemdanhgia) as 'Điểm đánh giá' from Sinhvien, Lop, Danhgia
	where Sinhvien.ID = Danhgia.SinhvienID
	and Sinhvien.LopID = Lop.ID
	and Ngaydanhgia = @Ngaydanhgia
	group by Sinhvien.ID,  Sinhvien.Ten, Ngaydanhgia, Lop.Ten
	order by Ngaydanhgia, Sinhvien.Ten
end

create proc [dbo].[allSinhvienByLop]
@LopID int 
as
begin
	select Sinhvien.ID, Sinhvien.Ten, Lop.Ten as 'Tên lớp', NgayDanhgia, SUM(Diemdanhgia) as 'Điểm đánh giá' from Sinhvien, Lop, Danhgia
	where Sinhvien.ID = Danhgia.SinhvienID
	and Sinhvien.LopID = Lop.ID
	and LopID = @LopID
	group by Sinhvien.ID,  Sinhvien.Ten, Ngaydanhgia, Lop.Ten
	order by Ngaydanhgia, Sinhvien.Ten
end

