import * as yup from 'yup';

const cameraModelSchema = yup.object({
  model: yup.string().required(),
  port: yup.string().required()
});

export const getCamerasResponseSchema = yup.object({
  cameras: yup.array().of(cameraModelSchema)
});

export type CameraModel = yup.InferType<typeof cameraModelSchema>
