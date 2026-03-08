import * as lark from '@larksuiteoapi/node-sdk';


const larkclient = new lark.Client({
    appId: 'cli_a9205191cbf89bc9',
    appSecret: 'H6fuhpGsKe7HJtq02tQ7UcmWhjqQIRtK',
})

larkclient.im.message.create({
    data: {
        receive_id: 'oc_9c1b',
        msg_type: '',
        content: ''
    }, 
    params: {
        receive_id_type: 'email'
    }
})