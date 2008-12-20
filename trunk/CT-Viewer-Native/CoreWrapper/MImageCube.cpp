#include "Stdafx.h"
#include "MImageCube.h"

using namespace System;
using namespace System::Runtime::InteropServices;


CoreWrapper::MImageCube::MImageCube(CoreNative::ImageCube* cube)
{
	this->cube = cube;
};

CoreWrapper::MImageCube::~MImageCube()
{

};

System::Guid CoreWrapper::MImageCube::ImageID::get()
{
	String^ managedString = Marshal::PtrToStringAnsi((IntPtr) (char*) cube->GetImageID());
	Guid^ guid = gcnew Guid(managedString);
	return *guid;
};

InterfaceLayer::Enumeration::ByteOrder CoreWrapper::MImageCube::ByteOrder::get()
{
	if(cube->GetByteOrder() == CoreNative::ImageCube::BYTEORDER::MSB)
	{
		return InterfaceLayer::Enumeration::ByteOrder::MSB;
	}
	else
	{
		return InterfaceLayer::Enumeration::ByteOrder::LSB;
	}
};

void CoreWrapper::MImageCube::ByteOrder::set(InterfaceLayer::Enumeration::ByteOrder value)
{
	if(value == InterfaceLayer::Enumeration::ByteOrder::MSB)
	{
		cube->SetByteOrder(CoreNative::ImageCube::BYTEORDER::MSB);
	}
	else
	{
		cube->SetByteOrder(CoreNative::ImageCube::BYTEORDER::LSB);
	}
};

InterfaceLayer::Enumeration::ImageOrientation CoreWrapper::MImageCube::ImageOrientation::get()
{
	if(cube->GetImageOrientation() == CoreNative::ImageCube::IMAGEORIENTATION::Frontal)
	{
		return InterfaceLayer::Enumeration::ImageOrientation::Frontal;
	}
	else if(cube->GetImageOrientation() == CoreNative::ImageCube::IMAGEORIENTATION::Sagittal)
	{
		return InterfaceLayer::Enumeration::ImageOrientation::Sagittal;
	}
	else
	{
		return InterfaceLayer::Enumeration::ImageOrientation::Transversal;
	}
};

void CoreWrapper::MImageCube::ImageOrientation::set(InterfaceLayer::Enumeration::ImageOrientation value)
{
	if(value == InterfaceLayer::Enumeration::ImageOrientation::Frontal)
	{
		cube->SetImageOrientation(CoreNative::ImageCube::IMAGEORIENTATION::Frontal);
	}
	else if(value == InterfaceLayer::Enumeration::ImageOrientation::Sagittal)
	{
		cube->SetImageOrientation(CoreNative::ImageCube::IMAGEORIENTATION::Sagittal);
	}
	else
	{
		cube->SetImageOrientation(CoreNative::ImageCube::IMAGEORIENTATION::Transversal);
	}
};

int CoreWrapper::MImageCube::DataSize::get()
{
	return cube->GetDataSize();
};

void CoreWrapper::MImageCube::DataSize::set(int value)
{
	cube->SetDataSize(value);
};

int CoreWrapper::MImageCube::Dimension::get()
{
	return cube->GetDimension();
};

void CoreWrapper::MImageCube::Dimension::set(int value)
{
	cube->SetDimension(value);
};

System::String^ CoreWrapper::MImageCube::HedFile::get()
{
	String ^ managedString = Marshal::PtrToStringAnsi((IntPtr) (char*) cube->GetHedFile());
	return managedString;
};

int CoreWrapper::MImageCube::DimX::get()
{
	return cube->GetDimX();
};

void CoreWrapper::MImageCube::DimX::set(int value)
{
	cube->SetDimX(value);
};

int CoreWrapper::MImageCube::DimY::get()
{
	return cube->GetDimY();
};

void CoreWrapper::MImageCube::DimY::set(int value)
{
	cube->SetDimY(value);
};

int CoreWrapper::MImageCube::DimZ::get()
{
	return cube->GetDimZ();
};

void CoreWrapper::MImageCube::DimZ::set(int value)
{
	cube->SetDimZ(value);
};

System::String^ CoreWrapper::MImageCube::CtxFile::get()
{
	String ^ managedString = Marshal::PtrToStringAnsi((IntPtr) (char*) cube->GetCtxFile());
	return managedString;
};

float CoreWrapper::MImageCube::PixelSize::get()
{
	return cube->GetDimZ();
};

void CoreWrapper::MImageCube::PixelSize::set(float value)
{
	cube->SetDimZ(value);
};

float CoreWrapper::MImageCube::SliceDistance::get()
{
	return cube->GetSliceDistance();
};

void CoreWrapper::MImageCube::SliceDistance::set(float value)
{
	cube->SetSliceDistance(value);
};

int CoreWrapper::MImageCube::OffsetX::get()
{
	return cube->GetOffsetX();
};

void CoreWrapper::MImageCube::OffsetX::set(int value)
{
	cube->SetOffsetX(value);
};

int CoreWrapper::MImageCube::OffsetY::get()
{
	return cube->GetOffsetY();
};

void CoreWrapper::MImageCube::OffsetY::set(int value)
{
	cube->SetOffsetY(value);
};

short*** CoreWrapper::MImageCube::CubeData::get()
{
	return cube->GetCubeData();
};


void CoreWrapper::MImageCube::CubeData::set(short*** value)
{
	cube->SetCubeData(value);
};

float CoreWrapper::MImageCube::GetBottomLineInMM()
{
	return cube->GetBottomLineInMM();
};

int CoreWrapper::MImageCube::GetBottomLineInPixel()
{
	return cube->GetBottomLineInPixel();
};

float CoreWrapper::MImageCube::GetTopLineInMM()
{
	return cube->GetTopLineInMM();
};

int CoreWrapper::MImageCube::GetTopLineInPixel()
{
	return cube->GetTopLineInPixel();
};

bool CoreWrapper::MImageCube::IsPixelDataAvailable()
{
	return cube->IsPixelDataAvailable();
};

short** CoreWrapper::MImageCube::GetSlice(InterfaceLayer::Enumeration::ImageOrientation orientation, int sliceIndex)
{
	CoreNative::ImageCube::IMAGEORIENTATION o = CoreNative::ImageCube::IMAGEORIENTATION::Transversal;
	if(orientation == InterfaceLayer::Enumeration::ImageOrientation::Frontal)
	{
		o = CoreNative::ImageCube::IMAGEORIENTATION::Frontal;
	}
	else if(orientation == InterfaceLayer::Enumeration::ImageOrientation::Sagittal)
	{
		o = CoreNative::ImageCube::IMAGEORIENTATION::Sagittal;
	}
	return cube->GetSlice(o,sliceIndex);
}

int CoreWrapper::MImageCube::TrafoScreenPosToSlice(int y)
{
	return cube->TrafoScreenPosToSlice(y);
};

int CoreWrapper::MImageCube::TrafoSliceToScreenPos(int slice)
{
	return cube->TrafoSliceToScreenPos(slice);
};