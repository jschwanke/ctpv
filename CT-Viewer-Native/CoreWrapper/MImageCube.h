//MImageCube
#include "Stdafx.h"
#include "..\CoreNative\ImageCube.h"

namespace CoreWrapper
{
	public ref class MImageCube : public InterfaceLayer::Image::IImageCube
	{
		private:
			CoreNative::ImageCube* cube;

		public:
			MImageCube(CoreNative::ImageCube*);
			~MImageCube();
			property System::Guid ImageID
			{
				virtual System::Guid get() = InterfaceLayer::Image::IImageCube::ImageID::get;
			};
			property InterfaceLayer::Enumeration::ByteOrder ByteOrder
			{
				virtual InterfaceLayer::Enumeration::ByteOrder get() = InterfaceLayer::Image::IImageCube::ByteOrder::get;
				virtual void set(InterfaceLayer::Enumeration::ByteOrder) = InterfaceLayer::Image::IImageCube::ByteOrder::set;
			};
			property InterfaceLayer::Enumeration::ImageOrientation ImageOrientation
			{
				virtual InterfaceLayer::Enumeration::ImageOrientation get() = InterfaceLayer::Image::IImageCube::ImageOrientation::get;
				virtual void set(InterfaceLayer::Enumeration::ImageOrientation) = InterfaceLayer::Image::IImageCube::ImageOrientation::set;	
			};
			property int DataSize
			{
				virtual int get() = InterfaceLayer::Image::IImageCube::DataSize::get;
				virtual void set(int) = InterfaceLayer::Image::IImageCube::DataSize::set;	
			};
			property int Dimension
			{
				virtual int get() = InterfaceLayer::Image::IImageCube::Dimension::get;
				virtual void set(int) = InterfaceLayer::Image::IImageCube::Dimension::set;	
			};
			property int DimX
			{
				virtual int get() = InterfaceLayer::Image::IImageCube::DimX::get;
				virtual void set(int) = InterfaceLayer::Image::IImageCube::DimX::set;	
			};
			property int DimY
			{
				virtual int get() = InterfaceLayer::Image::IImageCube::DimY::get;
				virtual void set(int) = InterfaceLayer::Image::IImageCube::DimY::set;	
			};
			property int DimZ
			{
				virtual int get() = InterfaceLayer::Image::IImageCube::DimZ::get;
				virtual void set(int) = InterfaceLayer::Image::IImageCube::DimZ::set;	
			};
			property System::String^ HedFile
			{
				virtual System::String^ get() = InterfaceLayer::Image::IImageCube::HedFile::get;
			};
			property System::String^ CtxFile
			{
				virtual System::String^ get() = InterfaceLayer::Image::IImageCube::CtxFile::get;
			};
			property float PixelSize
			{
				virtual float get() = InterfaceLayer::Image::IImageCube::PixelSize::get;
				virtual void set(float) = InterfaceLayer::Image::IImageCube::PixelSize::set;	
			};
			property float SliceDistance
			{
				virtual float get() = InterfaceLayer::Image::IImageCube::SliceDistance::get;
				virtual void set(float) = InterfaceLayer::Image::IImageCube::SliceDistance::set;	
			};
			property int OffsetX
			{
				virtual int get() = InterfaceLayer::Image::IImageCube::OffsetX::get;
				virtual void set(int) = InterfaceLayer::Image::IImageCube::OffsetX::set;	
			};
			property int OffsetY
			{
				virtual int get() = InterfaceLayer::Image::IImageCube::OffsetY::get;
				virtual void set(int) = InterfaceLayer::Image::IImageCube::OffsetY::set;	
			};
			property short*** CubeData
			{
				virtual short*** get() = InterfaceLayer::Image::IImageCube::CubeData::get;
				virtual void set(short***) = InterfaceLayer::Image::IImageCube::CubeData::set;	
			};
			virtual float GetBottomLineInMM() = InterfaceLayer::Image::IImageCube::GetBottomLineInMM;
			virtual int GetBottomLineInPixel() = InterfaceLayer::Image::IImageCube::GetBottomLineInPixel;
			virtual float GetTopLineInMM() = InterfaceLayer::Image::IImageCube::GetTopLineInMM;
			virtual int GetTopLineInPixel() = InterfaceLayer::Image::IImageCube::GetTopLineInPixel;
			virtual bool IsPixelDataAvailable() = InterfaceLayer::Image::IImageCube::IsPixelDataAvailable;
			virtual short** GetSlice(InterfaceLayer::Enumeration::ImageOrientation orientation, int sliceIndex) = InterfaceLayer::Image::IImageCube::GetSlice;
			virtual int TrafoScreenPosToSlice(int y) = InterfaceLayer::Image::IImageCube::TrafoScreenPosToSlice;
			virtual int TrafoSliceToScreenPos(int slice) = InterfaceLayer::Image::IImageCube::TrafoSliceToScreenPos;
	};
};
