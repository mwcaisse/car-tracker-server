<template>
    <div class="modal fade">
        <div class="modal-dialog" role="document" :style="{ 'max-width': width }">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">{{ title }}</h5>
                    <button type="button" class="close" aria-label="close" v-on:click="close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <slot>
                        <p>Yay for empty Modals</p>
                    </slot>
                </div>
                <div class="modal-footer">
                    <slot name="footer"></slot>
                    <button type="button" class="btn btn-secondary" v-on:click="close">Close</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

    export default {
        data: function () {
            return {
                modalOpen: false
            }
        },
        props: {
            title: {
                type: String,
                required: true
            },
            onClose: {
                type: Function,
                required: false,
                default: function () {
                    //default function, does nothing
                }
            },
            width: {
                type: String,
                required: false,
                default: "800px"
            }
        },        
        methods: {
            open: function () {
                //only call show if the modal isn't already open
                if (!this.modalOpen) {
                    $(this.$el).modal("show");
                }
            },
            close: function () {
                //only call hide if the modal is open
                if (this.modalOpen) {
                    $(this.$el).modal("hide");
                }
            }
        },
        mounted: function () {
            //subscribe to the model events so we know when it is open or closed
            $(this.$el).on("hide.bs.modal", function (e) {
                //check if this event is for this element, not a child modal/element
                if (e.target === this.$el) {
                    this.modalOpen = false;
                    this.onClose(); // perform any onClose actions
                }
            }.bind(this));

            $(this.$el).on("show.bs.modal", function (e) {
                if (e.target === this.$el) {
                    this.modalOpen = true;
                }
            }.bind(this));
        }
    }
</script>
